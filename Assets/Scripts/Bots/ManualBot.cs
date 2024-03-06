using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBot : Bot
{
    protected override void WinBehavior()
    {
        gameManager.PlayerWin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ProcessInput(Limbs.ARM_L);
        if (Input.GetKeyDown(KeyCode.H))
            ProcessInput(Limbs.ARM_R);
        if (Input.GetKeyDown(KeyCode.B))
            ProcessInput(Limbs.LEG_L);
        if (Input.GetKeyDown(KeyCode.N))
            ProcessInput(Limbs.LEG_R);

        base.Update();
    }
}
