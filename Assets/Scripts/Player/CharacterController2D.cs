using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController2D : MonoBehaviour
{
   
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, 2)] [SerializeField] private float m_Velocidade = 1;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    
    //variaveis de hp e dano
    public int vida = 100;
    public float coolDmg = 20f;
    public float coolPrlz = 15f;


    // componentes
    private Rigidbody2D m_Rigidbody2D;
    private ParticleSystem dashParticles = null;
    private TrailRenderer dashTrail = null;
    private Collision coll;
    public GameObject trailpointprefab;
    private SpriteRenderer sr;

    [Header("Variaveis de posição")]
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public Animator animControl;

    [Space]
    // variaveis de "pulo com intensidade"
   
    [Header("Variaveis de Pulo")]
    public float fallMult = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float grabMult = .1f;
    public float m_JumpForce = 9.8f;      // Amount of force added when the player jumps.                      

    // checks (booleanos)
    [Space]

    [Header("Checks")]
    private bool m_wasCrouching = false;
    public bool m_Pulou = false;
    public bool m_Dash = false;
    public bool m_DashCool = false;
    public bool dashInfinity = false;
    public bool m_sepcialfull;
    //public bool m_Grounded;
    public bool canMove;
    public bool ivenDmg;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool wallSlide = false;
    public bool wallGrab;

    // dash variaveis
    [Space]

    [Header("Variaveis de dash")]
    private float dashTimer;
    private List<Vector3> dashvertexs;
    //public float m_DashForce = 700f; 
    public float m_DashConstSpeed = 45f;
    public float maxDash = 35f; 
    public float dashRecharge = 0.1f;

    // variaveis de especial
    [Space]

    [Header("Variaveis da barra de especial")]
    public float m_special;  

//    [Space]

    //[Header("Events")]
    

//    public UnityEvent OnLandEvent;
//    [System.Serializable]
//    public class BoolEvent : UnityEvent<bool> { }
//    public BoolEvent OnCrouchEvent;
//    public Joystick controleMobile;



  


    private void Awake()
    {
        dashvertexs = new List<Vector3>(); 
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        animControl = GetComponent<Animator>();
        dashParticles =  GetComponentInChildren<ParticleSystem>();
        dashTrail =  GameObject.Find("Trail").GetComponent<TrailRenderer>();
        dashTrail.emitting = false;
        m_special = maxDash;
        m_sepcialfull = true;
        canMove = true;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       // #if UNITY_EDITOR
            float movimento = Input.GetAxisRaw("Horizontal");
            float yDash = Input.GetAxisRaw("Vertical");
     //   #endif

/*            
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
*/      
        if (Input.GetButtonDown("Jump") & (coll.onGround || coll.onWall) & canMove )
        {
            animControl.SetTrigger("pulou");
            m_Pulou = true;
        }
        else
        {
            m_Pulou = false;
        }
        
        
        
        if(Input.GetButton("Fire2") && (movimento != 0 || yDash != 0 ) && !m_DashCool && !coll.onWall && canMove) 
        {                                                         
            
            
            // ativou efeitos
            dashParticles.enableEmission = true;
            dashTrail.emitting = true;
            
            if(m_special == maxDash){
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
                FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
            }
            if(movimento < 0  && m_FacingRight){
                Flip();
            }
            if(movimento > 0  && !m_FacingRight){
                Flip();
            }
            animControl.SetTrigger("dash");
            dash(movimento,yDash);
            m_Dash = true;
            
            /*
            //realiza contas
            dashvertexs.Add(transform.position);
            int index = (((int)m_special) - 3  )*(-1); // salvando cada "vertice" do dash
            Debug.Log(index);
            //Debug.Log(dashvertexs[index]);
            */
        }
        else
        {
            if(canMove){
                Move(movimento * m_Velocidade, false, m_Pulou);
            }
            
        }
        animControl.SetFloat("velocidadeMov", movimento);
        animControl.SetFloat("velocidadeVertical", m_Rigidbody2D.velocity.y);
        animControl.SetBool("isGrounded", coll.onGround);
        animControl.SetBool("isWall", coll.onWall);
        
        //tentando fazer o flip que era para se feito na WallSlide!
        if(coll.onWall & !coll.onGround & wallSlide){
            Flip();
        }
        //animControl.SetInteger("wall1",coll.wallSide);
        //animControl.SetInteger("wall2",coll.wallSide);
    




    }



    private void FixedUpdate()
    {
       

        // calculos referentes a vida
        
        if(ivenDmg){
            //Debug.Log("estou invencivel");
            coolDmg -= 0.25f;
            if(coolDmg == 0){
                coolDmg = 20f;
                ivenDmg = false;
                Color temp = sr.color;
                temp.a = 1f;
                sr.color = temp;
            }
        }
        


        if(!canMove){
             coolPrlz -= 0.5f;
             if(coolPrlz == 0){
                coolPrlz = 15f;
                canMove = true;
             }
        }


        // calculos referentes ao dash
        // esta no modo dash  e nao esta no "modo infinito"
        if(m_Dash && !dashInfinity){
            m_special -= 0.5f;
            //Debug.Log("ESPECIAL:>"+m_special);
            if(m_special <= 0 ){
                m_special = 0;
                m_Dash = false;
                dashParticles.enableEmission = false;
                dashTrail.emitting = false;
                this.m_sepcialfull = false; 
            }

        }
        if(!this.m_sepcialfull){
            m_DashCool = true;
            specialrecharge();
           // Debug.Log("carregado");
        }



        // calculos referentes a queda(gravidade)
            // se esta grudado em alguma parede (na parede e no ar e indo em direção a ela)
            if(coll.onWall && !coll.onGround && Input.GetAxisRaw("Horizontal") != 0){   
                        WallSlide();
                        wallSlide = true;
                
            }
            //SENAO se aplica a dinamica normal de pulo (caindo ou pulando, podendo estar na parede)
            else{
        
                wallSlide = false;
                if(m_Rigidbody2D.velocity.y < 0 ){
                    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y*(fallMult-1)*Time.deltaTime;

                }
                else{
                     m_Rigidbody2D.velocity += Vector2.up *Physics2D.gravity.y*(lowJumpMultiplier-1)*Time.deltaTime;
                }
            }  
            //m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y*(grabMult)*Time.deltaTime;
        
        
    }



    void OnTriggerEnter2D(Collider2D other){
        // se ele nao esta no delay(invencibilidade) 
        if(!ivenDmg){
            // e é um penro nao esta dando dash
            if(other.tag.Equals("Penro") && other.GetType() != typeof(CircleCollider2D) && !m_Dash){
                Vector2 dir = other.attachedRigidbody.velocity;
                Dano(25,dir);
                
            }
            else
            {
                // ele esta dando dash (fica invencivel e ainda pode recarregar a barra de especial)
                if(other.tag.Equals("Penro") && other.GetType() != typeof(CircleCollider2D)){
                    Debug.Log("ABSOOOORVE");
                    m_special = m_special + 4f;
                }
            }
            // é um beta e nao esta dando dash 
            if(other.tag.Equals("Beta") && other.GetType() != typeof(CircleCollider2D) && !m_Dash){
                Vector2 dir = other.attachedRigidbody.velocity;
                Dano(25,dir);
            }

            // nao fica invencivel a tiros (apesar da trail proteger ele)
            // a nao ser que esteja no delay pós dano
            if(other.name.Equals("enemyBullet(Clone)")){
                Debug.Log("OUCH");
                Vector2 dir = other.attachedRigidbody.velocity;
                Dano(10,dir);
                Debug.Break();
            }
        }
    }


    // funções auxiliares

    public void Dano(int dano,Vector2 dir){
        //empurra o player e deixa ele temporariamente sem se mexer
        float push = (dir.x > 0)? 10f: -10f;
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Rigidbody2D.velocity = new Vector2(push,  0);
        canMove = false;
        ivenDmg = true;
        vida = vida - dano;
        Color temp = sr.color;
        temp.a = 0.5f;
        sr.color = temp;
    }


    public void pulo(bool ground, bool wall, bool rightW, bool leftW)
    {
        if(ground){
            m_Rigidbody2D.velocity += Vector2.up * m_JumpForce;
        }
        // era para funcionar pulo na parede... enfim ele pode dar dash da parede desde que ele "solte" da parede 
        // bom que isso balanceia o jogo...
        else{
            if(wall){
                Debug.Log("UEEE");
                float x;
                x = (rightW)? 1 : -1;
                //m_Rigidbody2D
                m_Rigidbody2D.velocity = new Vector2(x,0).normalized * m_JumpForce;
            }
        }

    }

    public void Move(float move, bool crouch, bool jump)
    {
     

        //only control the player if grounded or airControl is turned on
        if (coll.onGround || m_AirControl)
        {

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
        if (jump)
        {
            
            pulo(coll.onGround,wallSlide,coll.onRightWall,onLeftWall);
            m_Pulou = false;

        }

    }

    public void dash(float x,float y)
    {

        Instantiate(trailpointprefab,transform.position,transform.rotation);
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Rigidbody2D.velocity = new Vector2(x,y).normalized * m_DashConstSpeed;
        
    }

    public void specialrecharge(){
        if(this.m_special < maxDash){
            this.m_special += dashRecharge;
        }
        if(this.m_special >= 25){
            this.m_DashCool = false;
        }
        if(this.m_special >= maxDash){
            this.m_sepcialfull = true;
            this.m_special = maxDash;
        }
    }

    private void Flip()
    {
    
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f,180f,0f);
    }


    private void WallSlide()
    {
        //Debug.Log("entrou");
        //if(!m_FacingRight){
            //animControl.SetBool("isWall1", coll.onWall);
         //   Debug.Log("UE");
         //   Flip();
        //}
        //if(coll.wallSide == 1 & !m_FacingRight)
        //    Flip();

        //bool side =  (coll.wallSide == -1)? false : true;
        ///Debug.Log(side);
        //if(side != m_FacingRight){
        //    Debug.Log("ue");
        //    Flip();
        //}
        //if (!canMove)
        //    return;

        bool empurrandoParede = false;
        if((m_Rigidbody2D.velocity.x > 0 && coll.onRightWall) || (m_Rigidbody2D.velocity.x < 0 && coll.onLeftWall))
        {
            empurrandoParede = true;
        }

        //parede deve "empurrar de volta" para nao ter problema de ficar preso na parede
        float push = empurrandoParede ? 0 : m_Rigidbody2D.velocity.x;
        //float y = (wallSlide)? m_Rigidbody2D.velocity.y : 0;
        
        m_Rigidbody2D.velocity = new Vector2(push,  -grabMult);
        
        //teste
        /*
        Debug.Log(Time.deltaTime);
        Debug.Log(Vector2.up);
        Debug.Log(Physics2D.gravity.y);   
        Vector2 resultante = Vector2.up * Physics2D.gravity.y*(grabMult)*Time.deltaTime;
        Debug.Log(resultante);
        m_Rigidbody2D.velocity += resultante;
        */
    }

    /*
    IEnumerator DisabilitaMovimento(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    */
    
}