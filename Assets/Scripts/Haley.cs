using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haley : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string tempname = gameObject.name;
        gameObject.name = "Haley";
        if (GameObject.Find(tempname) != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            gameObject.name = tempname;
        }
    }
}
