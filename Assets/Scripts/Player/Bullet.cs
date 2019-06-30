using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D rb;
    private int damage = 30;
    // quando a bala da spawn
    void Start()
    {
        rb.velocity = transform.right*speed;
        Destroy(gameObject,5);

    } 

    void OnTriggerEnter2D(Collider2D other){

        Debug.Log(other.name);
        Enemy enemy = other.GetComponent<Enemy>(); // as vezes não seria melhor por o trigger no inimigo ?
                                                   //se bem que a bala é trigger ...
        if(enemy != null){
            enemy.Dano(damage);

        }
        Destroy(gameObject);
    }
  
}
