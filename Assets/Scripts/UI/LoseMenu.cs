using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] Button newGameBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] Image background;
    [SerializeField] Sprite sprite;
    [SerializeField] int waitTime;
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
        background.sprite = sprite;
        StartCoroutine(LoadProduction());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadProduction()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Production");
    }
}
