using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JannusBehaviour : ChampionsBase
{
    public override void ApplySpecial()
    {
        for(int i = 0; i < gameMotor.enemies.Length; i++)
        {
            if(gameMotor.enemies[i].transform.GetComponent<ChampionsBase>() != gameMotor.championUnderMouse)
            {
                ApplyDamage(1, gameMotor.enemies[i].transform.GetComponent<ChampionsBase>());
            }
        }
    }
}
