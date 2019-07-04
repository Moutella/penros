using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Secret2 : MonoBehaviour
{
    public string CenaASerCarregada = "Level2";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.name.Equals("Player") && !other.isTrigger){
    		//#ATIVA tiro novo
    		CharacterController2D ctrl = other.GetComponent<CharacterController2D>();
    		ctrl.triple = true;
    		ctrl.vida = 100;
    		ctrl.dashInfinity = false;
            Debug.Log("PLAYER ENTROU");
            if (!CenaASerCarregada.Equals(""))
            {
                GameObject player = GameObject.Find("Player");
                Debug.Log(player.transform.position);
                player.transform.position = Vector3.zero;
                Debug.Log(player.transform.position);
                SceneManager.LoadScene(CenaASerCarregada);
            }
    	}
    }
}
