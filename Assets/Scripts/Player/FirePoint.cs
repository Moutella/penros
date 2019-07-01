using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
   
    
    public GameObject bulletPrefab;
    private CharacterController2D playerCtrl;
    private int bulletCounter;
    public bool podeAtirar = true;
    void Awake(){
        playerCtrl = transform.parent.GetComponent<CharacterController2D>();
    }


    void Update()
    {
        
        podeAtirar = (bulletCounter < 3)? true : false;

        if(Input.GetButtonDown("Fire1") && podeAtirar){
            playerCtrl.animControl.SetTrigger("fire");
            bulletCounter++;
            Shoot();
            
            
        }    
        RecerragaTiro();
    }

    public void RecerragaTiro(){
        if(!podeAtirar)
        {
            bulletCounter--;
        }
    }
    void Shoot(){
        Instantiate(bulletPrefab,transform.position,transform.rotation);
    }
}
