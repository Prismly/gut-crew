using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementSchemes/Swim")]
public class Swim : MovementScheme
{
    public override Responses ProcessLimbData(LimbData inputData, Bot targetBot)
    {
        // Only accept Arm and Leg inputs.
        if (inputData.MostRecentActive != Bot.Limbs.LEG_L && inputData.MostRecentActive != Bot.Limbs.LEG_R
            && inputData.MostRecentActive != Bot.Limbs.ARM_L && inputData.MostRecentActive != Bot.Limbs.ARM_R)
            return Responses.NONE;

        // TODO: Investigate consequences of truly simultaneous inputs
        float leftLastLeg = inputData.LastActivatedAt[Bot.Limbs.LEG_L];
        float rightLastLeg = inputData.LastActivatedAt[Bot.Limbs.LEG_R];
        Bot.Limbs mostRecentLeg = leftLastLeg > rightLastLeg ? Bot.Limbs.LEG_L : Bot.Limbs.LEG_R;

        // If the activated leg is NOT the most recently activated leg (i.e. the back leg was moved), move.
        if ((inputData.MostRecentActive == Bot.Limbs.LEG_L || inputData.MostRecentActive == Bot.Limbs.LEG_R)
            && inputData.MostRecentActive != mostRecentLeg)
        {
            targetBot.GetAnimator().SetTrigger("Swim " + (mostRecentLeg == Bot.Limbs.LEG_L ? "Left" : "Right"));
            return Responses.MOVE;
        }
            

        // TODO: Investigate consequences of truly simultaneous inputs
        float leftLastArm = inputData.LastActivatedAt[Bot.Limbs.ARM_L];
        float rightLastArm = inputData.LastActivatedAt[Bot.Limbs.ARM_R];
        Bot.Limbs mostRecentArm = leftLastArm > rightLastArm ? Bot.Limbs.ARM_L : Bot.Limbs.ARM_R;

        if ((inputData.MostRecentActive == Bot.Limbs.ARM_L || inputData.MostRecentActive == Bot.Limbs.ARM_R)
            && inputData.MostRecentActive != mostRecentArm)
        {
            targetBot.GetAnimator().SetTrigger("Swim " + (mostRecentArm == Bot.Limbs.ARM_L ? "Left" : "Right"));
            return Responses.MOVE;
        }
            
        // If the activated leg/arm IS the most recently activated leg/arm (i.e. the front leg/arm was moved), stagger.
        return Responses.NONE;
    }
}
