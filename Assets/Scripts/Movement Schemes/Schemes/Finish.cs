using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementSchemes/Finish")]
public class Finish : MovementScheme
{
    public override Responses ProcessLimbData(LimbData inputData)
    {
        // Any input results in a win
        return Responses.WIN;
    }
}
