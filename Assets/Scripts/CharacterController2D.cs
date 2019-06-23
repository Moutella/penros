using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;   
    [SerializeField] private float m_DashForce = 100000f;                       // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, 2)] [SerializeField] private float m_Velocidade = 1;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public Animator animControl;
    //ma pratica? por public?

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    public ParticleSystem dashParticles = null;
    public TrailRenderer dashTrail = null;
    private bool m_wasCrouching = false;
    private bool m_Pulou = false;
    private bool m_Dash = false;
    private float dashTimer;
    private List<Vector3> dashvertexs;
    public Joystick controleMobile;

    private float m_special;  

    bool m_sepcialfull;
    private void Awake()
    {
        dashvertexs = new List<Vector3>(); 
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animControl = GetComponent<Animator>();
       // dashParticles = GetComponent<ParticleSystem>();
        dashTrail =  GameObject.Find("Trail").GetComponent<TrailRenderer>();
        dashTrail.emitting = false;
        m_special = 3f;
        m_sepcialfull = true;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    void Update()
    {
        #if UNITY_EDITOR
            float movimento = Input.GetAxisRaw("Horizontal");
        #endif


        #if UNITY_ANDROID
        if(controleMobile.Horizontal >= .3f)
            {
            movimento = 1;
            }
        else if (controleMobile.Horizontal <= -0.3f)
        {
            movimento = -1;
        }
                
        #endif
        //if (Input.GetButtonDown("Jump") & m_Grounded)
        //{
        //    animControl.SetTrigger("pulou");
        //    m_Pulou = true;
        //}
        //else
        //{
        //    m_Pulou = false;
        //  //  m_Dash = false;
        //}
        
        
        
        if(Input.GetKeyDown(KeyCode.O) && !(this.m_special < 1)) // to pensando se a condição de dash vai ser o specil full ou o tamanho
        {                                                         //se for o full ele só pode dar N dashes e depois não pode fazer nenhum até encher tudo
            Vector2 direcaoDash = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            animControl.SetTrigger("dash");
            dash(direcaoDash);
            m_Dash = true;

            dashParticles.enableEmission = true;
            
            dashTrail.emitting = true;

            dashTimer = Time.time;

            
            dashvertexs.Add(transform.position);
            int index = (((int)m_special) - 3  )*(-1); // salvando cada "vertice" do dash
            Debug.Log(index);
            //Debug.Log(dashvertexs[index]);

            m_special -= 1f;
            if(m_special < 1){
                m_special = 0f;
                m_sepcialfull = false;
            }
        }
        if (!m_Dash) { 
            Move(movimento * m_Velocidade, false, m_Pulou);
        }
        else
        {
            if(Time.time > dashTimer + 0.6f)
            {
                // agora que acabou o dash ele deve calcular o "ponto médio" dos locais que ele fez o dash
                float mx = 0.0f, my=0.0f,mz=0.0f;
                foreach (Vector3 v in dashvertexs)
                {
                    mx += v.x;
                    my += v.y;
                    mz += v.z;
                }
                mx = mx/dashvertexs.Count;
                my = my/dashvertexs.Count;
                mz = mz/dashvertexs.Count;
                Vector3 m_midpoint = new Vector3(mx,my,mz);
                Debug.Log("ponto médio: ");
                Debug.Log(m_midpoint);
                dashvertexs.Clear();

                dashParticles.enableEmission = false;
                const int MAX_POSITIONS = 100;
                Vector3[] TrailRecorded = new Vector3[MAX_POSITIONS];
                //Debug.Log(dashTrail.GetPositions(TrailRecorded));
                //Debug.Log(dashTrail.startWidth);
                //Debug.Log(dashTrail.endWidth);
                dashTrail.emitting = false;
                m_Dash = false;
                
            }
        }

        animControl.SetFloat("velocidadeMov", movimento);
        animControl.SetFloat("velocidadeVertical", m_Rigidbody2D.velocity.y);
        animControl.SetBool("isGrounded", m_Grounded);
    }
    private void FixedUpdate()
    {
       
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        if(!this.m_sepcialfull){
            Debug.Log(Time.time);
            Debug.Log("CARREGANDO: " + m_special);
            specialrecharge();
        }
        

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void pulo()
    {
        if (m_Grounded)
        {
            animControl.SetTrigger("pulou");
            m_Pulou = true;
        }
    }

    // funções auxiliares

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Pulou = false;
        }

    }

    public void dash(Vector3 direcao)
    {
        //m_Grounded = false; // nao faz sentido vc pular enquanto da o dash pelo menos eu imagino//o script ja  altera sozinho o m_Grounded
        direcao.Normalize();
        direcao *= m_DashForce;
        //m_Rigidbody2D.AddForce(direcao);
        //Debug.Log(direcao);
        m_Rigidbody2D.velocity = direcao/30;
    }

    public void specialrecharge(){
        if(this.m_special < 3.0f){
            this.m_special += 0.01f;
        }
        if(this.m_special >= 3.0f){
            this.m_sepcialfull = true;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f,180f,0f);
    }
}