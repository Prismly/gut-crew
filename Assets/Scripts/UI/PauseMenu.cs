using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void ResumeMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
