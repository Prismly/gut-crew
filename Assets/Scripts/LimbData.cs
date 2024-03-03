using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbData
{
    public Bot.Limbs MostRecentActive = Bot.Limbs.HEAD;
    public Dictionary<Bot.Limbs, float> LastActivatedAt;

    public LimbData()
    {
        LastActivatedAt = new Dictionary<Bot.Limbs, float>
        {
            { Bot.Limbs.HEAD, 0f },
            { Bot.Limbs.TORSO, 0f },
            { Bot.Limbs.ARM_L, 0f },
            { Bot.Limbs.ARM_R, 0f },
            { Bot.Limbs.LEG_L, 0f },
            { Bot.Limbs.LEG_R, 0f }
        };

    }
}
