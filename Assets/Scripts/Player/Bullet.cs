using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 45f;
    public Rigidbody2D rb;
    private int damage = 30;
    //private Transform parent;
    //quando a bala da spawn
    void Start()
    {
        //parent = transform.parent;
        rb.velocity = transform.right*speed;
        Destroy(gameObject,3);
        //parent.GetComponent<FirePoint>().RecerragaTiro();

    } 

    void OnTriggerEnter2D(Collider2D other){

        Debug.Log(other.name);
        Debug.Log(other.GetType());
        if(other.GetType() != typeof(CircleCollider2D)){
            Enemy enemy = other.GetComponent<Enemy>(); // as vezes não seria melhor por o trigger no inimigo ?
                                                   //se bem que a bala é trigger ...
            // note que os inimigos vao ter 2 colliders triggers (campo de visão e o hitbox normal)
            if(enemy != null){
                enemy.Dano(damage);

            }
            Destroy(gameObject);
        }
        
    }
  
}
