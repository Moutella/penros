using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public CharacterController2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
