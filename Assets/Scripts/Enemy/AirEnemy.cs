using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // tentando fazer ele colidir com a TRAIL!
    void OnTriggerEnter2D(Collider2D other){
    	Debug.Log(other.name);
    	
    }

    void OnTriggerStay2D(Collider2D other){
    	Debug.Log(other.name);
    }
}
