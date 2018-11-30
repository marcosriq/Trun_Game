using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoices : MonoBehaviour {


    public GameObject textbox, option01, option02, option03;
    public int optionSelected;

    public void ChoiceOption01()
    {
        textbox.GetComponent<Text>().text = "escolheu a escolha 1";
        optionSelected = 1;
    }

    public void ChoiceOption02()
    {
        textbox.GetComponent<Text>().text = "escolheu a escolha 2";
        optionSelected = 2;
    }

    public void ChoiceOption03()
    {
        textbox.GetComponent<Text>().text = "escolheu a escolha 3";
        optionSelected = 3;
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(optionSelected >= 1)
        {
            option01.SetActive(false);
            option02.SetActive(false);
            option03.SetActive(false);
        }
	}
}
