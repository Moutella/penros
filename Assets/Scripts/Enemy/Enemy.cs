using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // script "padrão" de inimigos note que se formos criar outros tipos de inimigos os scripts deles
    // devem herdar esse!

    // componentes
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public GameObject enemybulletprefab;
    public GameObject morreueffect;
    protected ParticleSystem shootParticles = null;

    //variaveis de estado
    public int vida = 100;
    public bool attackMode = false;
    public bool vendo = false;
    public float coolDown = 2f;
    public float modeDuration = 60f;
    public float moveSpeed = 5;
    public float shootSpeed = 15f;
    public int shootCaden;
    public bool shootLock;
    public bool moveLock;
    //private enum Acao{ padrao, movatac, tiro };
    public  int  acaoescolhida;


    //auxiliar 
    protected Vector2 originalpos;

    public void Dano(int dano){
        vida -= dano;
        if(vida <= 0){
            Debug.Log("NANI");
            Die();
        }
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalpos = transform.position;
        //esses efeitos nao ficaram legais nesse mob
        //shootParticles =  GetComponentInChildren<ParticleSystem>();
        //shootParticles.enableEmission = false; 
    }

    protected void Die(){
        //Instantiate(morreueffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void ModeUpdate(){
        if(attackMode){
            modeDuration = modeDuration - 0.1f;
            if(modeDuration <= 0){
                SwitchMode();
                originalpos = transform.position;
                Debug.Log("tO DE BOA");
            }
        }
    }
    protected void SwitchMode(){
        attackMode = !attackMode;
        //vendo = !vendo;
        modeDuration = 60f;
    }

    protected void StayMode(){
        attackMode = true;
        vendo = true;
        modeDuration = 60f;
    }

    protected void EscolheAcao(){
        if(attackMode){
            int r = Random.Range(1, 3);
            acaoescolhida = r;
        }
        else{
            acaoescolhida = 0;
        }
        
    }

    // retorna vetor em direção ao jogador
    protected Vector2 PersegueJogador(){
        GameObject jogador = GameObject.Find("Player");
        if(jogador != null){
            Vector3 pe = transform.position;
            Vector3 pp = jogador.transform.position;
            rb.velocity = Vector2.zero;
            Vector2 dir = new Vector2(pe.x-pp.x,pe.y-pp.y).normalized*(-1);
            return dir;
        }
        return Vector2.zero;
    }

    // basicamente avança contra o jogador (é um dos ataques sorteados caso o jogador entre no "campo de visao dele")
    protected void DirectTackle(){
        Vector2 dir = PersegueJogador();
        if(dir != Vector2.zero){
            rb.velocity = dir*(moveSpeed);
            Flip();
            
            
        }    
            
    }
        
    

    // atira no jogador (é um dos ataques sorteados caso o jogador entre no "campo de visão dele") (o que muda é a cadencia de tiro de cada um deles)
    protected void Shoot(){
        Vector2 dir = PersegueJogador();
        if(dir != Vector2.zero){
            Debug.Log(dir);
            //ativa efeitos
            for(int i =0; i < shootCaden; i++){
                GameObject tiroinimigo = Instantiate(enemybulletprefab,transform.position,transform.rotation) as GameObject;
                tiroinimigo.GetComponent<Rigidbody2D>().velocity = dir*shootSpeed;
            }        
        }
             
    } 

    protected void Flip(){
        if( ((rb.velocity.x) < 0) && sr.flipX){
            sr.flipX = !sr.flipX;
        }
        if(!sr.flipX && ((rb.velocity.x) > 0) ){
            sr.flipX = !sr.flipX;
        }
    } 
    
}


