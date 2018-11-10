using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionsBase : MonoBehaviour {

    public GameObject highlightChampEffect;
    public GameObject selectChampEffect;

    protected GameMotor gameMotor;
    public int life;
    public int damage;
    public int armor;
    public bool selected;

    protected void Start()
    {
        GameObject gm = GameObject.Find("GameMotor");
        gameMotor = gm.GetComponent<GameMotor>();
    }
    protected void Update()
    {
        if (selected)
        {
            gameMotor.ChampionSelected = this;
            selectChampEffect.SetActive(true);
        }
        else
            selectChampEffect.SetActive(false);

        if (this != gameMotor.championUnderMouse)
            highlightChampEffect.SetActive(false);

        if (life <= 0)
            Destroy(transform.parent.gameObject);
    }

    public void Attack(GameObject target)
    {

    }
}
