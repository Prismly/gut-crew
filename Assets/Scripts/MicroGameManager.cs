using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGameManager : MonoBehaviour
{
    [SerializeField] private Transform Player1Spawn;
    [SerializeField] private GameObject Player1Prefab;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void EndModulePlacement()
    {
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer()
    {
       yield return new WaitForSeconds(0.2f);
       GameObject player = Instantiate(Player1Prefab, Player1Spawn);
    }
}
