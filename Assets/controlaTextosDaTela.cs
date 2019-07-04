using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlaTextosDaTela : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> textosNaTela;
    public CharacterController2D player;
    public GameObject gameover;
    public float Morreu;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        Morreu = -10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.vida <= 0)
        {
            if(Morreu < 0f)
            {
                Morreu = Time.time;
                Debug.Log("SETOU TEMPO DA MORTE");
            }
            gameover.SetActive(true);
            if (Time.time >= (Morreu + 3f))
            {
                Debug.Log(Time.time - Morreu);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        textosNaTela[0].SetText("PV: " + Mathf.Clamp(player.vida, 0, 100));
        if (player.m_special>16.66666f)
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
