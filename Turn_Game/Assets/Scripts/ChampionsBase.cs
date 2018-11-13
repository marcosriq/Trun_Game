using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChampionsBase : MonoBehaviour {

    public GameObject highlightChampEffect;
    public GameObject selectChampEffect;

    protected GameMotor gameMotor;
    public int life;
    public int damage;
    public int armor;
    public bool selected;
    public bool isSilenced;
    public bool trueDamage;

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


    public void Attack(ChampionsBase target)
    {
        if (!isSilenced)
        {
            ApplySpecial();
        }

        if (trueDamage)
        {
            gameMotor.championUnderMouse.life -= damage;
        }
        else
        {
            int remainingDamage;
            if(gameMotor.championUnderMouse.armor != 0)
            {
                if (damage <= gameMotor.championUnderMouse.armor) {
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
        trueDamage = false;
    }
    public abstract void ApplySpecial();
}