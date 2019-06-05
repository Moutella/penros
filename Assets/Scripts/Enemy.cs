using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // script "padrão" de inimigos note que se formos criar outros tipos de inimigos os scripts deles
    // devem herdar esse!

    public int vida = 100;
    public GameObject morreueffect;

    public void Dano(int dano){
        vida -= dano;
        if(vida <= 0){
            Debug.Log("NANI");
            Die();
        }
    }

    void Die(){
        //Instantiate(morreueffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    } 
    
}
