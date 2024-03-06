using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button newGameBtn;
    [SerializeField] Button quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("LevelGrid");
        if (g != null)
        {
            Destroy(g);
        }

        GameObject g2 = GameObject.Find("LevelWalls");
        if (g2 != null)
        {
            Destroy(g2);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Production");
    }

    public void Quit()
    {
        Application.Quit();
    }    
}
