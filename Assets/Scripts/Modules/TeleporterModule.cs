using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterModule : ModuleBehavior
{
    protected override void Execute(GameObject Executor)
    {
        Vector3 positionOffset = Executor.transform.position - transform.position;
        Executor.transform.position = OtherModule.transform.position + positionOffset;
    }
}
