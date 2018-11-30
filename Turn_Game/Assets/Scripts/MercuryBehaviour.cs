using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryBehaviour : ChampionsBase {

    public override void ApplySpecial()
    {
        if (transform.tag == "Champion")
        {
            ApplyDamage(damage, gameMotor.championUnderMouse);
        }
        else if (transform.tag == "Enemy")
        {
            ApplyDamage(damage, gameMotor.enemyChampionUnderMouse);
        }
    }
}
