using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlaTextosDaTela : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> textosNaTela;
    public CharacterController2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        textosNaTela[0].SetText("VIDA");
        if (player.m_special>25)
        {
            textosNaTela[1].color = new Color(255, 255, 0, 255);
        }
        else
        {
            textosNaTela[1].color = new Color(255, 0, 0, 255);
        }
        if (player.m_sepcialfull)
        {
            textosNaTela[1].color = new Color(0, 255, 0, 255);
        }
        string dashStatus = "DASH: " + (Mathf.CeilToInt(player.m_special * 3.33f)).ToString() + "%";
        textosNaTela[1].SetText(dashStatus);
    }
}
