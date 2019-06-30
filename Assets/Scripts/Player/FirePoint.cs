using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
   
    
    public GameObject bulletPrefab;
    private CharacterController2D playerCtrl;
    private int bulletCounter;
    void Awake(){
        playerCtrl = transform.parent.GetComponent<CharacterController2D>();
    }


    void Update()
    {
        if(Input.GetButtonDown("Fire1") && bulletCounter < 3){
            playerCtrl.animControl.SetTrigger("fire");
            bulletCounter++;
            Shoot();
            bulletCounter--;
        }    
    }


    void Shoot(){
        Instantiate(bulletPrefab,transform.position,transform.rotation);
    }
}
