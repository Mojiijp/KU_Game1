using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HonesceneManager : MonoBehaviour
{
    public void GotoPlayGround()
    {
        SceneManager.LoadScene("Playground");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
