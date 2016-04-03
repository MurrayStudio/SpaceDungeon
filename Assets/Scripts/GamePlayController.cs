using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePlayController : MonoBehaviour {

	public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

	private Unit[] order;
	private Unit currentCharacter;
	private Unit unitClass;


	private Unit[] allies;
	private Unit[] enemies;

	private Enforcer enforcer;
	private Medic medic;
	private Rifleman rifleman;
	private Engineer engineer;

	public GameObject popUPAttack;
    public Button[] enemyAttackButtons = new Button[4];

    public Sprite enabledButtonImage;
    public Sprite disabledButtonImage;

    private bool popUPEnabled = false;
    private bool popUPAttackEnabled = false;

    private bool[] ability1EnableArray, ability2EnableArray, ability3EnableArray, ability4EnableArray, ability5EnableArray;

    private string currentButtonClicked;


    //This is assuming that there will be 4 enemies at 
    public Text enemy1Health;
    public Text enemy2Health;
    public Text enemy3Health;
    public Text enemy4Health;


    //This is assuming there will be four ally units
    public Text ally1Health;
    public Text ally2Health;
    public Text ally3Health;
    public Text ally4Health;
    
    


    // Use this for initialization
    void Start () {
		//init units
		enforcer = new Enforcer();
		enforcer.SetStats (1, 1, 100);
		medic = new Medic ();
		medic.SetStats (1, 1, 100);
		rifleman = new Rifleman ();
		rifleman.SetStats (1, 1, 100);
		engineer = new Engineer ();
		engineer.SetStats (1, 1, 100);

		allies = new Unit[] { enforcer, medic, rifleman, engineer };
		enemies = new Unit[] { enforcer, medic, rifleman, engineer };

		//hardcode setup for level 1
		//unitClass = new Unit();
		order = unitClass.Order(allies, enemies);

		//enforcer to start
		currentCharacter = allies [0];


        popUP.SetActive(false);
		popUPAttack.SetActive(false);

        //Assign health to each enemy + ally
        ally1Health.text = allies[0].GetHealth().ToString();
        ally2Health.text = allies[1].GetHealth().ToString();
        ally3Health.text = allies[2].GetHealth().ToString();
        ally4Health.text = allies[3].GetHealth().ToString();

        enemy1Health.text = enemies[0].GetHealth().ToString();
        enemy2Health.text = enemies[1].GetHealth().ToString();
        enemy3Health.text = enemies[2].GetHealth().ToString();
        enemy4Health.text = enemies[3].GetHealth().ToString();
      
        

        //for now, just have ability character attack configs enabled in this way
		ability1EnableArray = new bool[] { true, true, false, true, false, false, false };
		ability2EnableArray = new bool[] { false, false, false, true, false, false, false };
		ability3EnableArray = new bool[] { true, true, true, true, false, false, false };
		ability4EnableArray = new bool[] { true, true, false, false, false, false, false };
		ability5EnableArray = new bool[] { true, false, false, false, false, false, false };
    }
	
	// Update is called once per frame
	void Update () {
       
        //Assign health to each enemy + ally and constantly update
        ally1Health.text = allies[0].GetHealth().ToString();
        ally2Health.text = allies[1].GetHealth().ToString();
        ally3Health.text = allies[2].GetHealth().ToString();
        ally4Health.text = allies[3].GetHealth().ToString();

        enemy1Health.text = enemies[0].GetHealth().ToString();
        enemy2Health.text = enemies[1].GetHealth().ToString();
        enemy3Health.text = enemies[2].GetHealth().ToString();
        enemy4Health.text = enemies[3].GetHealth().ToString();
      
        
	}

	public void buttonClicked()
	{
        currentButtonClicked = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(currentButtonClicked);
        switch (currentButtonClicked)
        {
            case "ability1":
                showPopUpAttack();
                hidePopUp();
                break;
            case "ability2":
                showPopUpAttack();
                hidePopUp();
                break;
            case "ability3":
                showPopUpAttack();
                hidePopUp();
                break;
            case "ability4":
                showPopUpAttack();
                hidePopUp();
                break;
            case "ability5":
                showPopUpAttack();
                hidePopUp();
                break;
            case "Enemy1":
                hidePopUpAttack();
                hidePopUp();
                break;
            case "Enemy2":
                hidePopUpAttack();
                hidePopUp();
                break;
            case "Enemy3":
                hidePopUpAttack();
                hidePopUp();
                break;
            case "Enemy4":
                hidePopUpAttack();
                hidePopUp();
                break;
        }
    }

    public void showPopUpAttack()
    {
        //always move popUpAttack when clicked
        popUPAttack.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        //show attack popup
        if (!popUPAttackEnabled)
        {
            popUPAttack.SetActive(true);
            popUPAttackEnabled = true;
        }
        //config what buttons are enabled based off what ability selected:
        switch (currentButtonClicked)
        {
            case "ability1":
                for(int i=0; i < 4; i++)
                {
                    if (ability1EnableArray[i] == true) {
                        enemyAttackButtons[i].interactable = true;
                        enemyAttackButtons[i].image.sprite = enabledButtonImage;
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability2":
                for (int i = 0; i < 4; i++)
                {
                    if (ability2EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        enemyAttackButtons[i].image.sprite = enabledButtonImage;
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability3":
                for (int i = 0; i < 4; i++)
                {
                    if (ability3EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        enemyAttackButtons[i].image.sprite = enabledButtonImage;
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability4":
                for (int i = 0; i < 4; i++)
                {
                    if (ability4EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        enemyAttackButtons[i].image.sprite = enabledButtonImage;
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability5":
                for (int i = 0; i < 4; i++)
                {
                    if (ability5EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        enemyAttackButtons[i].image.sprite = enabledButtonImage;
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
        }

    }

    public void hidePopUpAttack()
    {
        popUPAttack.SetActive(false);
        popUPAttackEnabled = false;
    }

    public void showPopUp ()
    {
        if (!popUPEnabled)
        {
            popUP.SetActive(true);
            popUP.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            popUPEnabled = true;
        }
    }

    public void hidePopUp()
    {
        popUP.SetActive(false);
        popUPEnabled = false;
    }
}
