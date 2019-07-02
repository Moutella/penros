using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    public float time = 2;
    

    void Start()
    {
        //Destroy(gameObject,time);
    	// comentado para debug (nunca some)
    }

    void OnTriggerEnter2D(Collider2D other){

        Debug.Log(other.name);
        //Debug.Log(other.GetType());
        if(other.name.Equals("Inimigo-Octopus") && other.GetType() == typeof(CircleCollider2D)){
        	Debug.Log(other.name);
        }


        // trail com função de escudo! (note que por enquanto nao fiz o tiro dos inimigos então estou testando com os do player)
        // FUNCIONA tem que só trocar para o nome do projetil inimigo
        if(other.name.Equals("Bullet(Clone)")){
        	Destroy(other);
        	Destroy(gameObject);
        }

        /*
        if(other.GetType() != typeof(CircleCollider2D)){
            Enemy enemy = other.GetComponent<Enemy>(); // as vezes não seria melhor por o trigger no inimigo ?
                                                   //se bem que a bala é trigger ...
            // note que os inimigos vao ter 2 colliders triggers (campo de visão e o hitbox normal)
            if(enemy != null){
                enemy.Dano(damage);

            }
            Destroy(gameObject);
        }
        */
        
    }
    void OnTriggerStay2D(Collider2D other){
    	Debug.Log(other.name);
    	if(other.name.Equals("Inimigo-Octopus") && other.GetType() == typeof(CircleCollider2D)){
        	Debug.Log(other.name);
        }
    }
}
