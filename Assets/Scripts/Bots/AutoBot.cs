using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBot : Bot
{
    [SerializeField] private float ActionTimer = 1f;
    float timer = 0;

    private void Start()
    {
        timer = ActionTimer;
    }

    // Start is called before the first frame update
    private void Update()
    {
        base.Update();

        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            int rand = Random.Range(1, 5);
            if (rand == 0)
                ProcessInput(Limbs.ARM_L);
            else if (rand == 1)
                ProcessInput(Limbs.ARM_R);
            else if (rand == 2)
                ProcessInput(Limbs.LEG_L);
            else
                ProcessInput(Limbs.LEG_R);
            timer = ActionTimer;
        }
           
    }
}
