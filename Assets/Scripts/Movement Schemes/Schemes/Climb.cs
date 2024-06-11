using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementSchemes/Climb")]
public class Climb : MovementScheme
{
    public override Responses ProcessLimbData(LimbData inputData, Bot targetBot)
    {
        // Only accept Leg inputs.
        if (inputData.MostRecentActive != Bot.Limbs.ARM_L && inputData.MostRecentActive != Bot.Limbs.ARM_R)
        {
            return Responses.NONE;
        }

        // TODO: Investigate consequences of truly simultaneous inputs
        float leftLast = inputData.LastActivatedAt[Bot.Limbs.ARM_L];
        float rightLast = inputData.LastActivatedAt[Bot.Limbs.ARM_R];
        Bot.Limbs mostRecentArm = leftLast > rightLast ? Bot.Limbs.ARM_L : Bot.Limbs.ARM_R;

        // If the activated leg is NOT the most recently activated leg (i.e. the back leg was moved), move.
        if (inputData.MostRecentActive != mostRecentArm)
        {
            targetBot.GetAnimator().SetTrigger("Climb " + (mostRecentArm == Bot.Limbs.ARM_L ? "Left" : "Right"));
            return Responses.MOVE;
        }

        // If the activated leg IS the most recently activated leg (i.e. the front leg was moved), stagger.
        return Responses.NONE;
    }
}
