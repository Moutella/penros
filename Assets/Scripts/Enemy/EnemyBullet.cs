using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void OnTriggerEnter2D(Collider2D other){
        if(other.name.Equals("Player") || other.name.Equals("trailpointprefab(Clone)")){
            Destroy(gameObject); // bala inimiga acertou algo (escudo de trail ou player)
        }
        
        
    }
}
