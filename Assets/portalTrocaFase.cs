using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalTrocaFase : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public string CenaASerCarregada;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger)
        {
            Debug.Log("PLAYER ENTROU");
            if (!CenaASerCarregada.Equals(""))
            {
               
                GameObject player = GameObject.Find("Player");
                Debug.Log(player.transform.position);
                player.transform.position = Vector3.zero;
                Debug.Log(player.transform.position);
                CharacterController2D ctrl = player.GetComponent<CharacterController2D>();
                ctrl.dashInfinity = false;
                ctrl.vida = 100;
                SceneManager.LoadScene(CenaASerCarregada);
            }
        }
        else
        {
            abrePorta();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            Debug.Log("PLAYER SAIU");

        }
        else
        {
            fechaPorta();
        }
    }
        public void abrePorta()
    {
        anim.SetTrigger("abrePorta");
    }
    public void fechaPorta()
    {
        anim.SetTrigger("fechaPorta");
    }
}
