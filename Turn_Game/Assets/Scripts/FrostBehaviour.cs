using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBehaviour : ChampionsBase {

    public override void ApplySpecial()
    {
        if (transform.tag == "Champion")
        {
            if (gameMotor.championUnderMouse.GetComponent<FrostBehaviour>() == null)
            {
                gameMotor.championUnderMouse.isFrozen = true;
            }
        }
        else if (transform.tag == "Enemy")
        {
            if (gameMotor.enemyChampionUnderMouse.GetComponent<FrostBehaviour>() == null)
            {
                gameMotor.enemyChampionUnderMouse.isFrozen = true;
            }
        }
    }

}
