using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private List<GameObject> objects;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            transform.position = Vector3.Lerp(transform.position, obj.transform.position + offset, Time.deltaTime);
        }
    }
}
