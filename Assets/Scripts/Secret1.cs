using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.name.Equals("Player") && !other.isTrigger){
    		//#ATIVA EFEITO INFINITY
    		CharacterController2D player = other.GetComponent<CharacterController2D>();
    		player.dashInfinity = true;
    		Spama();
    	}
    	
    }

    void Spama(){

    }
}
