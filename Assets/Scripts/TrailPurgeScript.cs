using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPurgeScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
         Debug.Log(other.tag);
    }
    void OnTriggerStay2D(Collider2D other){
         Debug.Log(other.tag);
    }
}
