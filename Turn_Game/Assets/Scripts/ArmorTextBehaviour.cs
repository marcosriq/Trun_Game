using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorTextBehaviour : MonoBehaviour {


    public TextMesh textBorder;
    public ChampionsBase currentChampion;

	void Start () {
        currentChampion = transform.parent.GetComponentInChildren<ChampionsBase>();
	}
	
	void Update () {
        if (currentChampion.armor > 0) {
            GetComponent<TextMesh>().text = "" + currentChampion.armor;
            textBorder.text = "" + currentChampion.armor;
        }
        else
        {
            GetComponent<TextMesh>().text = "";
            textBorder.text = "";
        }
    }
}
