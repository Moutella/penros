using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controladorMenu : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Rigidbody2D rb;
    CharacterController2D ctrl;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
        ctrl = player.GetComponent<CharacterController2D>();
        ctrl.canMove = false;
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartaGame()
    {
        
        rb.simulated = true;
        ctrl.canMove = true;
        SceneManager.LoadScene("Level1");
    }
    public void FechaGame()
    {
        Application.Quit();
    }
}
