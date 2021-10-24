using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaygroundScenceManager : MonoBehaviour
{
    public Text uiTextCoin;
    public Text uiTextBullet; 
    public void GotoHomeScence()
    {
        SceneManager.LoadScene("Home");
    }

    public void SetTextCoin(int amount)
    {
        uiTextCoin.text = amount.ToString();
    }
    public void SetTextBullet(int amount)
    {
        uiTextBullet.text = amount.ToString();
    }
}
