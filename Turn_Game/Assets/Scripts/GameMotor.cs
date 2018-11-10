using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMotor : MonoBehaviour {

    public ChampionsBase championUnderMouse;
    public ChampionsBase ChampionSelected;
    public GameObject[] champions;
	// Use this for initialization
	void Start () {
        champions = GameObject.FindGameObjectsWithTag("Champion");
	}
	
	// Update is called once per frame
	void Update () {

        //////////Sistema de seleção e ataque \/
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            if (hit.transform != null)
            {
                if (hit.transform.tag == "Champion")
                {
                    //Guarda o campeão sob o mouse em uma variável global\/
                    championUnderMouse = hit.transform.GetComponent<ChampionsBase>();
                    if (!championUnderMouse.selected)
                        //Ativa o efeito de destaque do personagem sob o mouse\/
                        championUnderMouse.highlightChampEffect.SetActive(true);

                    if (ChampionSelected == null)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            unselectAll();
                            championUnderMouse.selected = true;
                            Debug.Log("You selected the " + hit.transform.name);
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (championUnderMouse == ChampionSelected)
                                unselectAll();
                            else
                            {
                                unselectAll();
                                championUnderMouse.selected = true;
                                Debug.Log("You selected the " + hit.transform.name);
                            }
                        }
                    }
                }
                else
                {
                    if (championUnderMouse != null)
                    {
                        //impede que o efeito de destaque seja ativado junto com o efeito de seleção\/
                        championUnderMouse.highlightChampEffect.SetActive(false);
                        championUnderMouse = null;
                    }
                }
                if (hit.transform.tag == "Enemy")
                {
                    //Verifica se há um campeão selecionado para que o inimigo possa ser selecionado como alvo\/
                    if (ChampionSelected != null)
                    {
                        hit.transform.GetComponent<ChampionsBase>().highlightChampEffect.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            hit.transform.GetComponent<ChampionsBase>().life -= ChampionSelected.damage;
                        }
                    }
                }
            }
            
        }
    }
    void unselectAll()
    {
        for(int i = 0;i < champions.Length;i++)
        {
            champions[i].GetComponent<ChampionsBase>().highlightChampEffect.SetActive(false);
            champions[i].GetComponent<ChampionsBase>().selected = false;
        }
        ChampionSelected = null;
    }
}
