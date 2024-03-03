using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMapController : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    private Renderer mapmaterial;
    [SerializeField]
    private Material basematerial;
    [SerializeField]
    private Material player1mat;
    [SerializeField]
    private Material player2mat;
    private bool player1occupied;
    private bool player2occupied;
    private float colorchange;

    // Start is called before the first frame update
    void Start()
    {
        mapmaterial = GetComponent<Renderer>();
        player1occupied = false;
        player2occupied = false;
        colorchange = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        colorchange += Time.deltaTime;
        if (colorchange >= 1f) {
            colorchange = 0f;
            if (player1occupied && !player2occupied) {
                if (mapmaterial.material.color != basematerial.color) {
                    mapmaterial.material = basematerial;
                }
                else {
                    mapmaterial.material = player1mat;
                }
            }
            else if (!player1occupied && player2occupied) {
                if (mapmaterial.material.color != basematerial.color) {
                    mapmaterial.material = basematerial;
                }
                else {
                    mapmaterial.material = player2mat;
                }
            }
            else if (player1occupied && player2occupied) {
                if (mapmaterial.material.color != player1mat.color) {
                    mapmaterial.material = player1mat;
                }
                else {
                    mapmaterial.material = player2mat;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        mapmaterial.material = basematerial;
        colorchange = 0f;
            GameObject otherObj = other.gameObject;
            if (otherObj == player1 || otherObj == player2) {
                if (otherObj == player1) {
                    player1occupied = true;
                }
                else if (otherObj == player2) {
                    player2occupied = true;
                }
            }
    }

    void OnTriggerExit(Collider other)
    {
        mapmaterial.material = basematerial;
        colorchange = 0f;
        GameObject otherObj = other.gameObject;
            if (otherObj == player1 || otherObj == player2) {
                if (otherObj == player1) {
                    player1occupied = false;
                }
                else if (otherObj == player2) {
                    player2occupied = false;
                }
            }
    }
}
