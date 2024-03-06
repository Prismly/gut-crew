using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // The time, in seconds, that the current race has been going on for.
    // Negative values indicate that a race is not currently in progress.
    private float raceTime = -1f;
    public float RaceTime { get { return raceTime; } }
    public bool RaceInProgress { get { return raceTime >= 0; } }

    [SerializeField] private float raceMaxTime = 600f;

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
    }

    public void BeginRace()
    {
        raceTime = 0;
    }

    public void EndRace()
    {
        raceTime = -1;
    }
}
