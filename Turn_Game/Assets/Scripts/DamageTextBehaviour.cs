using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextBehaviour : MonoBehaviour {


    public TextMesh textBorder;
    public ChampionsBase currentChampion;

	void Start () {
        currentChampion = transform.parent.GetComponentInChildren<ChampionsBase>();
	}
	
	void Update () {
        GetComponent<TextMesh>().text = "" + currentChampion.damage;
        textBorder.text = "" + currentChampion.damage;
    }
}
