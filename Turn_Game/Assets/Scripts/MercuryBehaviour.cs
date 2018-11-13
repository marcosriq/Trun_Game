using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryBehaviour : ChampionsBase {

    public override void ApplySpecial()
    {
        ApplyDamage(damage, gameMotor.championUnderMouse);
    }
}
