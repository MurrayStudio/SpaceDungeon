using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePlayController : MonoBehaviour
{

    public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

    //holds order of Units
    private Unit[] order;

    //holds currentCharacter
    private Unit currentCharacter;
    //holds currentAbility selected
    private int currentAbility;

    //holds current place in the order
    private int indexOfOrder;

    //holds Units
    private Unit[] allies;
    private Unit[] enemies;

    //Unit Classes
    private Enforcer enforcer;
    private Medic medic;
    private Rifleman rifleman;
    private Engineer engineer;

    private Freight freightEnemy1, freightEnemy2, freightEnemy3, freightEnemy4;
    private Infected infectedEnemy;
    private MediBot mediBotEnemy;
    private Psychic psychicEnemy;
    private Security securityEnemy;

    public Button[] enemyAttackButtons = new Button[4];
    public Button[] abilityButtons = new Button[5];
    public Sprite[] enforcerAbilityImages = new Sprite[5];
    public Sprite[] medicAbilityImages = new Sprite[5];
    public Sprite[] riflemanAbilityImages = new Sprite[5];
    public Sprite[] engineerAbilityImages = new Sprite[5];

    public Sprite enabledButtonImage;
    public Sprite disabledButtonImage;

    public GameObject popUPAttack;
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
    void Start()
    {
        //init units
        enforcer = new Enforcer();
        enforcer.SetStats(1, 1, 100);
        medic = new Medic();
        medic.SetStats(1, 1, 100);
        rifleman = new Rifleman();
        rifleman.SetStats(1, 1, 100);
        engineer = new Engineer();
        engineer.SetStats(1, 1, 100);
        freightEnemy1 = new Freight();
        freightEnemy2 = new Freight();
        freightEnemy3 = new Freight();
        freightEnemy4 = new Freight();

        allies = new Unit[] { enforcer, medic, rifleman, engineer };
        enemies = new Unit[] { freightEnemy1, freightEnemy2, freightEnemy3, freightEnemy4 };

        //hardcode setup for level 1
        order = enforcer.Order(allies, enemies);

        Debug.Log(order[0].GetType().ToString());
        Debug.Log(order[1].GetType().ToString());
        Debug.Log(order[2].GetType().ToString());
        Debug.Log(order[3].GetType().ToString());
        Debug.Log(order[4].GetType().ToString());
        Debug.Log(order[5].GetType().ToString());
        Debug.Log(order[6].GetType().ToString());

        //enforcer to start
        indexOfOrder = 0;
        currentCharacter = order[indexOfOrder];


        //popUP.SetActive(false);
        //popUPAttack.SetActive(false);
        hidePopUp();
        hidePopUpAttack();

        ability1EnableArray = currentCharacter.GetAttackRange(0);
        ability2EnableArray = currentCharacter.GetAttackRange(1);
        ability3EnableArray = currentCharacter.GetAttackRange(2);
        ability4EnableArray = currentCharacter.GetAttackRange(3);
        ability5EnableArray = currentCharacter.GetAttackRange(4);

    }

    // Update is called once per frame
    void Update()
    {

        currentCharacter = order[indexOfOrder];

        //Assign health to each enemy + ally and constantly update
        ally1Health.text = allies[0].GetHealth().ToString();
        ally2Health.text = allies[1].GetHealth().ToString();
        ally3Health.text = allies[2].GetHealth().ToString();
        ally4Health.text = allies[3].GetHealth().ToString();


        enemy1Health.text = enemies[0].GetHealth().ToString();
        enemy2Health.text = enemies[1].GetHealth().ToString();
        enemy3Health.text = enemies[2].GetHealth().ToString();
        enemy4Health.text = enemies[3].GetHealth().ToString();

        //get attack ranges
        if (currentCharacter.GetFriendly() == true)
        {
            Debug.Log("current character: " + currentCharacter.GetType().ToString());
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                abilityButtons[0].image.sprite = enforcerAbilityImages[0];
                abilityButtons[1].image.sprite = enforcerAbilityImages[1];
                abilityButtons[2].image.sprite = enforcerAbilityImages[2];
                abilityButtons[3].image.sprite = enforcerAbilityImages[3];
                abilityButtons[4].image.sprite = enforcerAbilityImages[4];
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                abilityButtons[0].image.sprite = medicAbilityImages[0];
                abilityButtons[1].image.sprite = medicAbilityImages[1];
                abilityButtons[2].image.sprite = medicAbilityImages[2];
                abilityButtons[3].image.sprite = medicAbilityImages[3];
                abilityButtons[4].image.sprite = medicAbilityImages[4];
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                abilityButtons[0].image.sprite = engineerAbilityImages[0];
                abilityButtons[1].image.sprite = engineerAbilityImages[1];
                abilityButtons[2].image.sprite = engineerAbilityImages[2];
                abilityButtons[3].image.sprite = engineerAbilityImages[3];
                abilityButtons[4].image.sprite = engineerAbilityImages[4];
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                abilityButtons[0].image.sprite = riflemanAbilityImages[0];
                abilityButtons[1].image.sprite = riflemanAbilityImages[1];
                abilityButtons[2].image.sprite = riflemanAbilityImages[2];
                abilityButtons[3].image.sprite = riflemanAbilityImages[3];
                abilityButtons[4].image.sprite = riflemanAbilityImages[4];
            }

            //ability1EnableArray = currentCharacter.GetAttackRange(0);
            //ability2EnableArray = currentCharacter.GetAttackRange(1);
            //ability3EnableArray = currentCharacter.GetAttackRange(2);
            //ability4EnableArray = currentCharacter.GetAttackRange(3);
            //ability5EnableArray = currentCharacter.GetAttackRange(4);
        }

        Debug.Log("after attack ranges: " + ability1EnableArray[0]);
        Debug.Log("after attack ranges: " + ability1EnableArray[1]);
        Debug.Log("after attack ranges: " + ability1EnableArray[2]);
        Debug.Log("after attack ranges: " + ability1EnableArray[3]);

        if (currentCharacter.GetFriendly() == false)
        {
            //make a simple ai move
            currentCharacter.MakeMove(0, allies, enemies, allies[0]);
            if (indexOfOrder < order.Length - 1)
            {
                ++indexOfOrder;
            }
            else
            {
                indexOfOrder = 0;
            }
        }
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
                currentAbility = 0;
                break;
            case "ability2":
                showPopUpAttack();
                hidePopUp();
                currentAbility = 1;
                break;
            case "ability3":
                showPopUpAttack();
                hidePopUp();
                currentAbility = 2;
                break;
            case "ability4":
                showPopUpAttack();
                hidePopUp();
                currentAbility = 3;
                break;
            case "ability5":
                showPopUpAttack();
                hidePopUp();
                currentAbility = 4;
                break;
            //clicking to attack characters
            case "Enemy1":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[0]);
                    ++indexOfOrder;
                }
                break;
            case "Enemy2":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[1]);
                    ++indexOfOrder;
                }
                break;
            case "Enemy3":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[2]);
                    ++indexOfOrder;
                }
                break;
            case "Enemy4":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[3]);
                    ++indexOfOrder;
                }
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
                for (int i = 0; i < 4; i++)
                {
                    if (ability1EnableArray[i] == true)
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

    public void showPopUp()
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
