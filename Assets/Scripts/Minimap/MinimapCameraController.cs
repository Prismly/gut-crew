using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + 3);
    }
}
