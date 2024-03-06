using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // The time, in seconds, that the current race has been going on for.
    // Negative values indicate that a race is not currently in progress.
    private float raceTime = -1f;
    public float RaceTime { get { return raceTime; } }
    public bool RaceInProgress { get { return raceTime >= 0; } }

    [SerializeField] private float raceMaxTime = 600f;

    [SerializeField] private TMP_Text WinText;
    [SerializeField] private TMP_Text LoseText;

    string winner = "";
    float TransitionTimer = -1;

    private void Awake()
    {
        Instance = this;
        BeginRace();
    }

    private void Update()
    {
        if (raceTime >= 0)
            raceTime += Time.deltaTime;

        if (raceTime >= raceMaxTime)
            raceTime = -1;
        
        if (winner != "")
        {
            if (TransitionTimer > 0)
                TransitionTimer -= Time.deltaTime;
            else if (winner == "player")
                SceneManager.LoadScene(2);
            else if (winner == "enemy")
                SceneManager.LoadScene(3);
        }
    }

    public void BeginRace()
    {
        raceTime = 0;
    }

    public void EndRace()
    {
        raceTime = -1;
    }

    public void PlayerWin()
    {
        if (winner != "")
            return;

        WinText.gameObject.SetActive(true);
        winner = "player";
        TransitionTimer = 2;
    }

    public void EnemyWin()
    {
        if (winner != "")
            return;

        LoseText.gameObject.SetActive(true);
        winner = "enemy";
        TransitionTimer = 2;
    }    
}
