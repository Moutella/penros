using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controladorMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartaGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void FechaGame()
    {
        Application.Quit();
    }
}
