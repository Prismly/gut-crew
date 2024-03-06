using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBot : Bot
{
    float ActionTimer = 0;
    float timer = 0;

    private Limbs LastAction = Limbs.TORSO;

    protected override void WinBehavior()
    {
        gameManager.EnemyWin();
    }

    private void Start()
    {
        ActionTimer = GameObject.Find("LevelGrid").GetComponent<Global>().EnemyActionTimer;
    }

    // Start is called before the first frame update
    private void Update()
    {
        base.Update();

        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            MovementScheme Objective = myTrack.GetSegmentAtDist(CurrentDist).MoveScheme;
            TryMove(Objective);
            
            timer = ActionTimer;
        }
           
    }

    public void TryMove(MovementScheme Objective)
    {
        if (Objective is Climb)
        {
            if (LastAction == Limbs.ARM_R)
            {
                ProcessInput(Limbs.ARM_L);
                LastAction = Limbs.ARM_L;
            }
            else
            {
                ProcessInput(Limbs.ARM_R);
                LastAction = Limbs.ARM_R;
            }
        }
        else
        {
            if (LastAction == Limbs.LEG_R)
            {
                ProcessInput(Limbs.LEG_L);
                LastAction = Limbs.LEG_L;
            }
            else
            {
                ProcessInput(Limbs.LEG_R);
                LastAction = Limbs.LEG_R;
            }
        }
    }
}
