using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMotor : MonoBehaviour {

    public ChampionsBase championUnderMouse;
    public ChampionsBase championSelected;
    public GameObject[] enemies;
    public GameObject[] champions;
    public Text counterText;
    public bool myTurn;
    public float turnTimer;
    public int turnCount;

	void Start () {
        turnCount = 1;
        champions = GameObject.FindGameObjectsWithTag("Champion");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        turnTimer = 30;
	}
	
	// Update is called once per frame
	void Update () {
        turnTimer -= Time.deltaTime;
        counterText.text = "" + Mathf.FloorToInt(turnTimer);
        if (turnTimer <= 0)
            SwitchTurn();

        //Sistema de seleção e ataque \/
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

                    if (championSelected == null)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (myTurn)
                            {
                                if (!championUnderMouse.isFrozen)
                                {
                                    unselectAll();
                                    championUnderMouse.selected = true;
                                    Debug.Log("You selected the " + hit.transform.name);
                                }
                                else
                                {
                                    Debug.Log("Este campeão está congelado neste turno");
                                }
                            }
                            else
                            {
                                Debug.Log("Ainda não é seu turno");
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (myTurn)
                            {
                                if (!championUnderMouse.isFrozen)
                                {
                                    if (championUnderMouse == championSelected)
                                        unselectAll();
                                    else
                                    {
                                        unselectAll();
                                        championUnderMouse.selected = true;
                                        Debug.Log("You selected the " + hit.transform.name);
                                    }
                                }
                                else
                                {
                                    Debug.Log("Este campeão está congelado neste turno");
                                }
                            }
                            else
                            {
                                Debug.Log("Ainda não é seu turno");
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
                    //Guarda o campeão sob o mouse em uma variável global\/
                    championUnderMouse = hit.transform.GetComponent<ChampionsBase>();

                    //Verifica se há um campeão selecionado para que o inimigo possa ser selecionado como alvo\/
                    if (championSelected != null)
                    {
                        championUnderMouse.highlightChampEffect.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            championSelected.Attack(championUnderMouse);
                            print(championUnderMouse.name + " recebeu " + championSelected.damage + " de dano de " + championSelected.name);
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
        championSelected = null;
    }
    public void SwitchTurn()
    {
        myTurn = !myTurn;
        turnTimer = 30;
        turnCount += 1;
    }
}
