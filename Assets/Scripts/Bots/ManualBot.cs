using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBot : Bot
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IncrementDistance(2f);
        }
    }
}
