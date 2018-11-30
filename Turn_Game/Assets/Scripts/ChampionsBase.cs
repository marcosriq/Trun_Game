using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChampionsBase : MonoBehaviour {
    [System.Serializable]
    public class Attachables
    {
        public GameObject highlightChampEffect;
        public GameObject selectChampEffect;
        public GameObject frozenEffect;
    }
    protected GameMotor gameMotor;
    public int life;
    public int damage;
    public int armor;
    public bool selected;
    public bool isSilenced;
    public bool trueDamage;
    public bool isFrozen;
    public Attachables attachables;

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
            attachables.selectChampEffect.SetActive(true);
        }
        else
            attachables.selectChampEffect.SetActive(false);

        if (this != gameMotor.championUnderMouse)
            attachables.highlightChampEffect.SetActive(false);

        if (life <= 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
        if (isFrozen)
        {
            attachables.frozenEffect.SetActive(true);
        }
        else
        {
            attachables.frozenEffect.SetActive(false);
        }
    }

    public void ApplyDamage(int dmg, ChampionsBase target)
    {
        if (trueDamage)
        {
            target.life -= dmg;
            CallHitText(dmg, target);
        }
        else
        {
            int remainingDamage;
            if (target.armor != 0)
            {
                if (dmg <= target.armor)
                {
                    target.armor -= dmg;
                    CallHitText(dmg, target);
                }
                else
                {
                    remainingDamage = dmg - target.armor;
                    target.armor = 0;
                    target.life -= remainingDamage;
                    CallHitText(dmg, target);
                }
            }
            else
            {
                target.life -= dmg;
                CallHitText(dmg, target);
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
    public virtual void ApplySpecial() { }

    void CallHitText(int dmg, ChampionsBase target)
    {
        if (GetComponent<MercuryBehaviour>() == null)
        {
            GameObject txt;
            txt = Instantiate(gameMotor.hitText);
            txt.transform.SetParent(target.transform);
            txt.transform.position = new Vector3(0, 0, 0);
            txt.transform.rotation = new Quaternion(0, 0, 0, 0);
            txt.transform.GetComponent<TextMesh>().text = "-" + dmg;
        }
        else
        {
            GameObject txt;
            TextMesh[] childTxt;
            txt = Instantiate(gameMotor.mercuryHitText, target.transform.position, target.transform.rotation, target.transform);
            childTxt = txt.GetComponentsInChildren<TextMesh>();
            for (int i = 0; i < childTxt.Length; i++)
            {
                childTxt[i].text = "-" + dmg;
            }
        }
    }
    private void OnDisable()
    {
        if (transform.tag == "Champion")
        {
            gameMotor.championsAlive -= 1;
        }
        if (transform.tag == "Enemy")
        {
            gameMotor.enemiesAlive -= 1;
        }
    }
}