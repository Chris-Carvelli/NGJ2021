using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("click was registered");
    }

    public void quitGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }
}
