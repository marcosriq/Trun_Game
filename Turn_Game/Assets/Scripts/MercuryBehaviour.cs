using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryBehaviour : ChampionsBase {

    public override void ApplySpecial()
    {
        int remainingDamage;
        if (gameMotor.championUnderMouse.armor != 0)
        {
            if (damage <= gameMotor.championUnderMouse.armor)
            {
                gameMotor.championUnderMouse.armor -= damage;
            }
            else
            {
                remainingDamage = damage - gameMotor.championUnderMouse.armor;
                gameMotor.championUnderMouse.armor = 0;
                gameMotor.championUnderMouse.life -= remainingDamage;
            }
        }
        else
        {
            gameMotor.championUnderMouse.life -= damage;
        }
    }
}
