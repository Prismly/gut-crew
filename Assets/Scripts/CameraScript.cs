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
        
        if(Input.GetKey(KeyCode.F))
        {
            objects[0].transform.position = Vector3.Lerp(objects[0].transform.position, new Vector3(3, (float)-13.5, 0), Time.deltaTime);
            objects[1].transform.position = Vector3.Lerp(objects[1].transform.position, new Vector3(5, -20, 0), Time.deltaTime);
        }
    }
}
