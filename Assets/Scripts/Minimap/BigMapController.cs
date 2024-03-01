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


    // Start is called before the first frame update
    void Start()
    {
        mapmaterial = GetComponent<Renderer>();
    }

    // // Update is called once per frame
    // void FixedUpdate()
    // {
        
    // }

    IEnumerator OnTriggerStay(Collider other)
    {
            GameObject otherObj = other.gameObject;
            if (otherObj == player1 || otherObj == player2) {
                if (otherObj == player1) {
                    if (mapmaterial.material.color != basematerial.color) {
                        mapmaterial.material = basematerial;
                    }
                    else {
                        mapmaterial.material = player1mat;
                    }
                }
                else if (otherObj == player2) {
                    if (mapmaterial.material.color != basematerial.color) {
                        mapmaterial.material = basematerial;
                    }
                    else {
                        mapmaterial.material = player2mat;
                    }
                }
            }
        
        yield return new WaitForSeconds(5);
    }
}
