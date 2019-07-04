using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    public float time = 2;
    

    void Start()
    {
        Destroy(gameObject,time);
    	// comentado para debug (nunca some)
    }

    void OnTriggerEnter2D(Collider2D other){

        
        if(other.tag.Equals("Penro") && other.GetType() != typeof(CircleCollider2D)){
        	Enemy enemy = other.GetComponent<Enemy>();
        	if(enemy != null){
        		enemy.Dano(100);
        	}
        }
        if(other.tag.Equals("Beta") && other.GetType() != typeof(CircleCollider2D)){
        	Enemy enemy = other.GetComponent<Enemy>();
        	enemy.moveSpeed *= -0.5f;
        }


        // trail com função de escudo! (note que por enquanto nao fiz o tiro dos inimigos então estou testando com os do player)
        // FUNCIONA tem que só trocar para o nome do projetil inimigo
        if(other.name.Equals("enemyBullet(Clone)")){
        	//Destroy(other);
        	Destroy(gameObject);
        }
        
    }
    void OnTriggerStay2D(Collider2D other){
    	
    	if(other.name.Equals("Inimigo-Octopus") && other.GetType() != typeof(CircleCollider2D)){
        	Debug.Log("purificado");
        }
    }
}
