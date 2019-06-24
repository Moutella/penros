using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
   
    
    public GameObject bulletPrefab;
    private CharacterController2D playerCtrl;
    void Awake(){
        playerCtrl = transform.parent.GetComponent<CharacterController2D>();

    }


    void Update()
    {
        if(Input.GetButtonDown("Fire1") && playerCtrl.m_Grounded){
            playerCtrl.animControl.SetTrigger("fire");
            Shoot();
        }    
    }


    void Shoot(){
        Instantiate(bulletPrefab,transform.position,transform.rotation);
    }
}
