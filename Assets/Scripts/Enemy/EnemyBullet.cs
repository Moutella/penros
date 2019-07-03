using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void OnTriggerEnter2D(Collider2D other){
        
        // trail defendeu o player
        if(other.name.Equals("trailpointprefab(Clone)")){
            Destroy(gameObject); 
        }
        
        // player toma dano
        /*
        if(other.name.Equals("Player") &&  other.GetType() != typeof(CircleCollider2D)){
        	CharacterController2D player = other.GetComponent<CharacterController2D>();
        	if(player != null){
        		if(!player.m_Dash && !player.ivenDmg){
        			Vector2 dir = rb.velocity;
        			player.Dano(30,dir);
        		}
        	}
        }
        */
        
        
        
    }
}
