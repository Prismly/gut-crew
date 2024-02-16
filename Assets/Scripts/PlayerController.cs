using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.started + "\n" + context.performed + "\n" + context.canceled);
    }
}
