using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : ModuleBehavior
{
    [SerializeField] private ManualBot bot;
    [SerializeField] private Bot.Limbs limb;

    protected override void Execute(GameObject Executor)
    {
        bot.ProcessInput(limb);
    }
}
