using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementSchemes/Swim")]
public class Swim : MovementScheme
{
    public override Responses ProcessLimbData(LimbData inputData)
    {
        // Only accept Leg inputs.
        if (inputData.MostRecentActive != Bot.Limbs.LEG_L && inputData.MostRecentActive != Bot.Limbs.LEG_R)
            return Responses.NONE;

        // TODO: Investigate consequences of truly simultaneous inputs
        float leftLast = inputData.LastActivatedAt[Bot.Limbs.LEG_L];
        float rightLast = inputData.LastActivatedAt[Bot.Limbs.LEG_R];
        Bot.Limbs mostRecentLeg = leftLast > rightLast ? Bot.Limbs.LEG_L : Bot.Limbs.LEG_R;

        // If the activated leg is NOT the most recently activated leg (i.e. the back leg was moved), move.
        if (inputData.MostRecentActive != mostRecentLeg)
            return Responses.MOVE;

        // If the activated leg IS the most recently activated leg (i.e. the front leg was moved), stagger.
        return Responses.STAGGER;
    }
}
