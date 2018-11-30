using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JannusBehaviour : ChampionsBase
{
    public override void ApplySpecial()
    {
        if (transform.tag == "Champion")
        {
            for (int i = 0; i < gameMotor.enemyChampions.Length; i++)
            {
                if (gameMotor.enemyChampions[i].transform.parent.gameObject.activeInHierarchy)
                {
                    if (gameMotor.enemyChampions[i].transform.GetComponent<ChampionsBase>() != gameMotor.championUnderMouse)
                    {
                        ApplyDamage(1, gameMotor.enemyChampions[i].transform.GetComponent<ChampionsBase>());
                    }
                }
            }
        }else if(transform.tag == "Enemy")
        {
            for (int i = 0; i < gameMotor.myChampions.Length; i++)
            {
                if (gameMotor.myChampions[i].transform.parent.gameObject.activeInHierarchy)
                {
                    if (gameMotor.myChampions[i].transform.GetComponent<ChampionsBase>() != gameMotor.enemyChampionUnderMouse)
                    {
                        ApplyDamage(1, gameMotor.myChampions[i].transform.GetComponent<ChampionsBase>());
                    }
                }
            }
        }
    }
}
