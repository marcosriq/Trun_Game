
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsisBehaviour : ChampionsBase {

    public override void ApplySpecial()
    {
        gameMotor.championUnderMouse.isSilenced = true;
    }

}
