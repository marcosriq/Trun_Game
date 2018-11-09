using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMotor : MonoBehaviour {

    public ChampionsBase ChampionSelected;
    [HideInInspector]
    public int selectedChampions;

    public GameObject[] champions;
    private bool canAttack;
	// Use this for initialization
	void Start () {
        champions = GameObject.FindGameObjectsWithTag("Champion");
        canAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag == "Champion")
                {
                    if (selectedChampions == 0)
                    {
                        hit.transform.GetComponent<ChampionsBase>().selected = true;
                        Debug.Log("You selected the " + hit.transform.name);
                    }
                    else {
                        unselectAll();
                        hit.transform.GetComponent<ChampionsBase>().selected = true;
                    }
                }
                if(hit.transform.tag == "Enemy")
                {
                    if (selectedChampions != 0)
                    {
                        hit.transform.GetComponent<ChampionsBase>().life -= ChampionSelected.damage;
                    }
                }
            }
        }
        if (selectedChampions != 0)
        {
            canAttack = true;
        }
    }
    
    void unselectAll()
    {
        for(int i = 0;i < champions.Length;i++)
        {
            champions[i].GetComponent<Champion>().selected = false;
        }
        selectedChampions = 0;
        ChampionSelected = null;
    }
}
