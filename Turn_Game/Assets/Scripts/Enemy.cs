using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ChampionsBase {

	// Use this for initialization
	void Start () {
        GameObject gm = GameObject.Find("GameMotor");
        gameMotor = gm.GetComponent<GameMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        if(selected)
        {
            transform.localPosition = new Vector3(0, 0, -0.003f);
            gameMotor.selectedChampions += 1;
            gameMotor.ChampionSelected = this;
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
