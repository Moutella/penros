﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaEnemy : Enemy
{
    // Start is called before the first frame update
   	public int maxSpeed = 8;

    // Update is called once per frame
    void Update()
    {
    	 EscolheAcao();
    	 if(coolDown > 10f){
        	// libera locks
        	shootLock = false;
        	moveLock = false;
        	moveSpeed = (moveSpeed < maxSpeed)? maxSpeed : moveSpeed;
        }
        else{
        	coolDown = coolDown - 0.1f;
        	if(coolDown <= 0){
        		coolDown = 11f;
        	}
        	if(coolDown <= 6){
        		moveLock = true;
        	}
        }

        if(acaoescolhida == 0 && !attackMode){
        	DefaultMove();
        	coolDown = coolDown - 0.1f;
        }

        if(acaoescolhida == 1 && vendo){
        	DirectTackle();
        }
        if(acaoescolhida < 2 && attackMode && !shootLock){
        	Shoot();
        	coolDown = coolDown - 0.1f;
        	shootLock = true;
        
        }
        
    }

    void FixedUpdate(){
    	ModeUpdate(); 
    }

        void OnTriggerEnter2D(Collider2D other){
    	if(other.name.Equals("Player") && !other.isTrigger){
    		//#ATIVA EFEITO VE O JOGADOR
    		SwitchMode();
    		Debug.Log("TO TE VENDO FILHO DA PUTA");
    	}
    	
    }


     void OnTriggerStay2D(Collider2D other){
    	if(other.name.Equals("Player") && !other.isTrigger)
        {
    		//vendo = true;
    		StayMode();
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.name.Equals("Player") && !other.isTrigger)
        {
    		vendo = false;
    		
    	}
    }


    private void DefaultMove(){
    	
    	float x; 
    	x = 10f; 
    	if(moveLock ){
    		x = x*(-1);
    	}
    	Vector2 dir = new Vector2(x,0).normalized;
    	rb.velocity = dir*(moveSpeed);
    	Flip();
    	
    }
}