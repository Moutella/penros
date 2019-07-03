using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : Enemy
{
   
   	//public float offsetradio;

    // Update is called once per frame
    void Update()
    {
        

        EscolheAcao();
        if(coolDown == 11f){
        	// libera locks
        	shootLock = false;
        	//moveLock = false;
        	
        }else{
        	coolDown = coolDown - 0.1f;
        	if(coolDown <= 0){
        		coolDown = 11f;
        	}
        }
        

        // ações do mob
        /*
        if(acaoescolhida == 0 && !attackMode){
        	DefaultMove();
        	coolDown = coolDown - 0.1f;
        }
		*/  // descartado

        if(acaoescolhida == 1 && vendo){
        	DirectTackle();
        }
        if(acaoescolhida == 2 && attackMode && !shootLock){
        	Shoot();
        	coolDown = coolDown - 0.1f;
        	shootLock = true;
        
        }
       
    }

    void FixedUpdate(){
    	ModeUpdate(); 
    }

    
    void OnTriggerEnter2D(Collider2D other){
    	if(other.name.Equals("Player")){
    		//#ATIVA EFEITO VE O JOGADOR
    		SwitchMode();
    		Debug.Log("TO TE VENDO FILHO DA PUTA");
    	}
    	
    }

    void OnTriggerStay2D(Collider2D other){
    	if(other.name.Equals("Player")){
    		//vendo = true;
    		StayMode();
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.name.Equals("Player")){
    		vendo = false;
    		
    	}
    }

    /*
    private void DefaultMove(){
    	
    	float x; 
    	x = (originalpos.x != transform.position.x && moveLock)? (originalpos.x-transform.position.x) + Random.Range(0,offsetradio)  : Random.Range(0, 10);
    	float y; 
    	y = (originalpos.y != transform.position.y && moveLock)? (originalpos.y-transform.position.y) + Random.Range(0,offsetradio) : Random.Range(0, 10);
    	Vector2 dir = new Vector2(x,y).normalized;
    	rb.velocity = dir*(moveSpeed);
    	//Flip();
    	
    }
    */
}
