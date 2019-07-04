using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
   
    
    public GameObject bulletPrefab;
    private CharacterController2D playerCtrl;
    private int bulletCounter;
    private int charge = 0;
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
        if(Input.GetButton("Fire1") && playerCtrl.triple)
        {
            charge = charge +1;
            Debug.Log(charge);
            if(charge > 200){
                ShootExtra();
                charge = 0;
                playerCtrl.animControl.SetTrigger("fire");
            }
               
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

    void ShootExtra(){
        Instantiate(bulletPrefab,transform.position,transform.rotation);
        Instantiate(bulletPrefab,transform.position,  Quaternion.Euler(new Vector3(0, 0, 15))  );
        Instantiate(bulletPrefab,transform.position,  Quaternion.Euler(new Vector3(0, 0, 30))  );
        Instantiate(bulletPrefab,transform.position,  Quaternion.Euler(new Vector3(0, 0, -30)) );
        Instantiate(bulletPrefab,transform.position,  Quaternion.Euler(new Vector3(0, 0, -15))  );
    }
}
