using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private bool snap = true;
    [SerializeField] private Vector3 offset;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private float lerpSpeed = 3;

    // Update is called once per frame
    void Update()
    {
        Vector3 averagePosition = Vector3.zero;
        for (int i = 0; i < objects.Count; i++)
        {
            averagePosition += objects[i].transform.position;
        }

        if (snap)
        {
            transform.position = averagePosition + offset;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, averagePosition + offset, Time.deltaTime * lerpSpeed);
        }
    }
}
