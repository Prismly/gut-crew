using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceModule : ModuleBehavior
{
    [SerializeField] private float BounceImpulse = 30f;

    protected override void Execute(GameObject Executor)
    {
        PlayerController player;
        if (player = Executor.GetComponent<PlayerController>())
        {
            player.Jump(BounceImpulse);
        }
    }
}
