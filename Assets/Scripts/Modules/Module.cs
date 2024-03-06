using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : ObjectGrid
{
    [SerializeField] private GameObject GrayThomas;

    private bool Active = true;


    private void Awake()
    {
        foreach (Tile tile in tiles)
        {
            //tile.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetActive(bool active)
    {
        Active = active;
        
        if (Active)
            GrayThomas.SetActive(false);
        else
            GrayThomas.SetActive(true);
    }
}
