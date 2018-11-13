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
            gameMotor.championSelected = this;
            selectChampEffect.SetActive(true);
        }
        else
            selectChampEffect.SetActive(false);

        if (this != gameMotor.championUnderMouse)
            highlightChampEffect.SetActive(false);

        if (life <= 0)
            Destroy(transform.parent.gameObject);
    }

    public void ApplyDamage(int dmg, ChampionsBase target)
    {
        if (trueDamage)
        {
            target.life -= dmg;
        }
        else
        {
            int remainingDamage;
            if (target.armor != 0)
            {
                if (dmg <= target.armor)
                {
                    target.armor -= dmg;
                }
                else
                {
                    remainingDamage = dmg - target.armor;
                    target.armor = 0;
                    target.life -= remainingDamage;
                }
            }
            else
            {
                target.life -= dmg;
            }
        }
    }

    public void Attack(ChampionsBase target)
    {
        if (!isSilenced)
            ApplySpecial();

        ApplyDamage(damage, target);
        trueDamage = false;
    }
    public abstract void ApplySpecial();
}