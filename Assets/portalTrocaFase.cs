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
            if (CenaASerCarregada != null)
            {
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
