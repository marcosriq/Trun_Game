using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMotor : MonoBehaviour {
    public GameObject endGameScreen;
    public Text endScreenText;
    [HideInInspector]
    public GameObject[] enemyChampions;
    [HideInInspector]
    public GameObject[] myChampions;
    public ChampionsBase championUnderMouse;
    public ChampionsBase championSelected;
    public ChampionsBase enemyChampionUnderMouse;
    public GameObject hitText;
    public GameObject mercuryHitText;
    public Image crosshair;
    public Text counterText;
    public Button turnButton;
    public int championsAlive, enemiesAlive;
    public bool myTurn;
    public float turnTimer;
    public int turnCount;
    public float enemySelectChampDelay = 0;
    public float enemySelectTgtDelay = 0;
    public float enemyAttackDelay = 0;

    void Start () {
        championsAlive = 3;
        enemiesAlive = 3;
        crosshair.enabled = false;
        turnCount = 1;
        myChampions = GameObject.FindGameObjectsWithTag("Champion");
        enemyChampions = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject tButton = GameObject.FindGameObjectWithTag("TurnButton");
        turnButton = tButton.GetComponent<Button>();
        turnTimer = 31;
        myTurn = true;
    }
	
	// Update is called once per frame
	void Update () {
        crosshair.transform.position = Input.mousePosition;
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
                    {
                        //Ativa o efeito de destaque do personagem sob o mouse\/
                        championUnderMouse.attachables.highlightChampEffect.SetActive(true);
                        crosshair.enabled = false;
                    }

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
                                    crosshair.enabled = true;
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
                        championUnderMouse.attachables.highlightChampEffect.SetActive(false);
                        if(championSelected != null)
                        {
                            crosshair.enabled = true;
                        }
                    }
                    if (hit.transform.tag == "Enemy")
                    {
                        //Guarda o campeão sob o mouse em uma variável global\/
                        championUnderMouse = hit.transform.GetComponent<ChampionsBase>();

                        //Verifica se há um campeão selecionado para que o inimigo possa ser selecionado como alvo\/
                        if (championSelected != null)
                        {
                            championUnderMouse.attachables.highlightChampEffect.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                championSelected.Attack(championUnderMouse);
                                print(championUnderMouse.name + " recebeu " + championSelected.damage + " de dano de " + championSelected.name);
                                unselectAll();
                                SwitchTurn();
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            unselectAll();
                        }
                    }
                }
            }
            
        }
        //Verifica se há apenas um campeão, e se ele estiver congelado, o jogo acaba
        if(championsAlive == 1)
        {
            for(int i=0; i<3; i++)
            {
                if (myChampions[i].activeInHierarchy)
                {
                    if (myChampions[i].GetComponent<ChampionsBase>().isFrozen)
                    {
                        Debug.Log("Você Perdeu!!!");
                        endScreenText.text = "Você Perdeu!!!";
                        endGameScreen.SetActive(true);
                        this.enabled = false;
                    }
                }
            }
        }
        //Verifica se há apenas um inimigo, e se ele estiver congelado, o jogo acaba
        if (enemiesAlive == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (enemyChampions[i].activeInHierarchy)
                {
                    if (enemyChampions[i].GetComponent<ChampionsBase>().isFrozen)
                    {
                        Debug.Log("Você Venceu!!!");
                        endScreenText.text = "Você Venceu!!!";
                        endGameScreen.SetActive(true);
                        this.enabled = false;
                    }
                }
            }
        }
        //Verifica se todos os inimigos morreram e encerra o jogo
        if (enemiesAlive == 0)
        {
            Debug.Log("Você Venceu!!");
            endScreenText.text = "Você Venceu!!!";
            endGameScreen.SetActive(true);
            this.enabled = false;
        }
        //Verifica se todos os campeões morreram e encerra o jogo
        if (championsAlive == 0)
        {
            Debug.Log("Você Perdeu!!");
            endScreenText.text = "Você Perdeu!!!";
            endGameScreen.SetActive(true);
            this.enabled = false;
        }
        if (!myTurn)
        {
            turnButton.interactable = false;
            turnButton.transform.GetComponentInChildren<Text>().text = "Turno do Inimigo";
            EnemyTurn();
        }
        else
        {
            turnButton.interactable = true;
            turnButton.transform.GetComponentInChildren<Text>().text = "Finalizar Turno";
        }
    }

    public void EnemyTurn()
    {
        GameObject[] championsAvaliable, targetsAvaliable;
        int championsAlive, targetsAlive, chooseChampion, chooseTarget;
        championsAvaliable = new GameObject[0];
        targetsAvaliable = new GameObject[0];
        championsAlive = 0;
        targetsAlive = 0;
        //dispara um contador de delay gerado aleatoriamente
        enemyAttackDelay -= Time.deltaTime;
        if (enemyAttackDelay <= 0f)
        {
            enemyAttackDelay = 0;
            //loop para o inimigo verificar quantos campeoes e inimigos estão disponiveis
            for (int i = 0; i < 3; i++)
            {
                if (enemyChampions[i].transform.parent.gameObject.activeInHierarchy && !enemyChampions[i].GetComponent<ChampionsBase>().isFrozen)
                {
                    championsAlive += 1;
                }
                if (myChampions[i].transform.parent.gameObject.activeInHierarchy)
                {
                    targetsAlive += 1;
                }
            }
            //array de tamanho equivalente a quantidade de campeoes e inimigos disponiveis
            championsAvaliable = new GameObject[championsAlive];
            targetsAvaliable = new GameObject[targetsAlive];
            //loop para verificar as posições disponiveis da array e anexar os campeões inimigos disponiveis a ela
            for (int i = 0; i < 3; i++)
            {
                if (enemyChampions[i].transform.parent.gameObject.activeInHierarchy && !enemyChampions[i].GetComponent<ChampionsBase>().isFrozen)
                {
                    int x = 0;//variavel acrescida a cada posição ocupada da array
                    while (championsAvaliable[x] != null)
                    {
                        x += 1;
                    }
                    if (championsAvaliable[x] == null)
                    {
                        championsAvaliable[x] = enemyChampions[i];
                    }
                }
            }
            //loop para verificar as posições disponiveis da array e anexar seus alvos disponiveis a ela
            for (int i = 0; i < 3; i++)
            {
                if (myChampions[i].transform.parent.gameObject.activeInHierarchy)
                {
                    int x = 0;
                    while (targetsAvaliable[x] != null)
                    {
                        x += 1;
                    }
                    if (targetsAvaliable[x] == null)
                    {
                        targetsAvaliable[x] = myChampions[i];
                    }
                }
            }
            //Escolhe aleatoriamente um campeão e um alvo entre os que estão disponiveis nas respectivas arrays
            chooseChampion = Random.Range(0, championsAlive);
            chooseTarget = Random.Range(0, targetsAlive);

            enemyChampionUnderMouse = targetsAvaliable[chooseTarget].transform.GetComponent<ChampionsBase>();
            championsAvaliable[chooseChampion].transform.GetComponent<ChampionsBase>().Attack(enemyChampionUnderMouse);
            Debug.Log(championsAvaliable[chooseChampion].transform.name + " atacou " + enemyChampionUnderMouse.transform.name);
            SwitchTurn();
        }
    }

    void unselectAll()
    {
        for(int i = 0;i < myChampions.Length;i++)
        {
            if (myChampions[i] != null)
            {
                myChampions[i].GetComponent<ChampionsBase>().attachables.highlightChampEffect.SetActive(false);
                myChampions[i].GetComponent<ChampionsBase>().selected = false;
            }
        }
        championSelected = null;
        crosshair.enabled = false;
    }
    public void SwitchTurn()
    {
        enemyAttackDelay = 0;
        enemyAttackDelay = Random.Range(2f, 7f);
        if (myTurn)
        {
            for (int i = 0; i < 3; i++)
            {
                myChampions[i].GetComponent<ChampionsBase>().isFrozen = false;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                enemyChampions[i].GetComponent<ChampionsBase>().isFrozen = false;
            }
        }
        myTurn = !myTurn;
        turnTimer = 31;
        turnCount += 1;
        //Gera um valor de delay aleatório para o ataque do turno inimigo, a fim de suavizar a transição e aparentar indecisão
        enemyAttackDelay = Random.Range(1f, 2f);
    }
}
