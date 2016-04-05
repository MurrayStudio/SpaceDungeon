using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePlayController : MonoBehaviour
{
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

    //Enemy Unit Classes
    private Freight freightEnemy1, freightEnemy2, freightEnemy3, freightEnemy4;
    private Infected infectedEnemy;
    private MediBot mediBotEnemy;
    private Psychic psychicEnemy;
    private Security securityEnemy;

    //UI Buttons
    public Button[] enemyAttackButtons = new Button[4];
    public Button[] abilityButtons = new Button[5];

    //what array to enable in pop up attack
    private bool[] ability1EnableArray, ability2EnableArray, ability3EnableArray, ability4EnableArray, ability5EnableArray;

    //Images for UI
    public Sprite[] enforcerAbilityImages = new Sprite[5];
    public Sprite[] medicAbilityImages = new Sprite[5];
    public Sprite[] riflemanAbilityImages = new Sprite[5];
    public Sprite[] engineerAbilityImages = new Sprite[5];
    public Sprite enabledButtonImage;
    public Sprite disabledButtonImage;

    //popUp window and components
    public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

    //holds text of current character
    public Text currentCharacterText;

    //popup attack window
    public GameObject popUPAttack;

    //UI pop up bools
    private bool popUPEnabled = false;
    private bool popUPAttackEnabled = false;

    //current button
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


    //ability info
    public Text ability1;
    public Text ability2;
    public Text ability3;
    public Text ability4;
    public Text ability5;


	public GameObject currentCharacterArrow;

	//get coordinate info about characters on screen
	public GameObject[] characters;

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

		currentCharacterArrow.transform.position = new Vector3(characters [indexOfOrder].transform.position.x, characters [indexOfOrder].transform.position.y + 1.5f, characters [indexOfOrder].transform.position.z);

		Debug.Log (characters [indexOfOrder].transform.position);

        currentCharacterText.text = "Current Character: " + currentCharacter.GetType().ToString();

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

            //lucas needs to fix unit classes first

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
            int move = Random.Range(0, 5);
            int allyHit = Random.Range(0, 4);
            currentCharacter.MakeMove(move, allies, enemies, allies[allyHit]);
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

    public void showPopUp(int ability)
    {
        if (ability == 0)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability1.text = "HEAVY SWING" + "\n" + "Crit: " + enforcer.CRIT_MODS[0].ToString() + "%" + "\n" + "Damage: " + (enforcer.DMG_MODS[0] * 100).ToString() + "\n" + "Accuracy: " + enforcer.ACC_MODS[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability1.text = "ADRENALINE" + "\n" + "Crit: " + medic.CRIT_MODS[0].ToString() + "%"+"\n" + "Damage: " + (medic.DMG_MODS[0] * 100).ToString() + "\n" + "Accuracy: " + medic.ACC_MODS[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability1.text = "FLASH BANG" + "\n" + "Crit: " + engineer.CRIT_MODS[0].ToString() + "%"+"\n" + "Damage: " + (engineer.DMG_MODS[0] * 100).ToString() + "\n" + "Accuracy: " + engineer.ACC_MODS[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability1.text = "BAYONET STAB" + "\n" + "Crit: " + rifleman.CRIT_MODS[0].ToString() + "%"+"\n" + "Damage: " + (rifleman.DMG_MODS[0] * 100).ToString() + "\n" + "Accuracy: " + rifleman.ACC_MODS[0].ToString();
            }
        }

        if (ability == 1)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability2.text = "KICK" + "\n" + "Crit: " + enforcer.CRIT_MODS[1].ToString() + "%" + "\n" + "Damage: " + (enforcer.DMG_MODS[1] * 100).ToString() + "\n" + "Accuracy: " + enforcer.ACC_MODS[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability2.text = "BULWARK" + "\n" + "Crit: " + medic.CRIT_MODS[1].ToString() + "%"+"\n" + "Damage: " + (medic.DMG_MODS[1] * 100).ToString() + "\n" + "Accuracy: " + medic.ACC_MODS[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability2.text = "ION PULSE" + "\n" + "Crit: " + engineer.CRIT_MODS[1].ToString() +"%"+ "\n" + "Damage: " + (engineer.DMG_MODS[1] * 100).ToString() + "\n" + "Accuracy: " + engineer.ACC_MODS[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability2.text = "NET" + "\n" + "Crit: " + rifleman.CRIT_MODS[1].ToString() + "%"+"\n" + "Damage: " + (rifleman.DMG_MODS[1] * 100).ToString() + "\n" + "Accuracy: " + rifleman.ACC_MODS[1].ToString();
            }
        }


        if (ability == 2)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability3.text = "SLICE" + "\n" + "Crit: " + enforcer.CRIT_MODS[2].ToString() + "%" + "\n" + "Damage: " + (enforcer.DMG_MODS[2] * 100).ToString() + "\n" + "Accuracy: " + enforcer.ACC_MODS[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability3.text = "HEALING WAVE" + "\n" + "Crit: " + medic.CRIT_MODS[2].ToString() + "%"+"\n" + "Damage: " + (medic.DMG_MODS[2] * 100).ToString() + "\n" + "Accuracy: " + medic.ACC_MODS[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability3.text = "LIGHT WALL" + "\n" + "Crit: " + engineer.CRIT_MODS[2].ToString() + "%"+"\n" + "Damage: " + (engineer.DMG_MODS[2] * 100).ToString() + "\n" + "Accuracy: " + engineer.ACC_MODS[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability3.text = "RELOAD" + "\n" + "Crit: " + rifleman.CRIT_MODS[2].ToString() + "%"+"\n" + "Damage: " + (rifleman.DMG_MODS[2] * 100).ToString() + "\n" + "Accuracy: " + rifleman.ACC_MODS[2].ToString();
            }
        }

        if (ability == 3)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability4.text = "STEROIDS" + "\n" + "Crit: " + enforcer.CRIT_MODS[3].ToString() + "%" + "\n" + "Damage: " + (enforcer.DMG_MODS[3] * 100).ToString() + "\n" + "Accuracy: " + enforcer.ACC_MODS[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability4.text = "PISTOL SHOT" + "\n" + "Crit: " + medic.CRIT_MODS[3].ToString() + "%"+"\n" + "Damage: " + (medic.DMG_MODS[3] * 100).ToString() + "\n" + "Accuracy: " + medic.ACC_MODS[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability4.text = "RATCHET GUN" + "\n" + "Crit: " + engineer.CRIT_MODS[3].ToString() + "%"+"\n" + "Damage: " + (engineer.DMG_MODS[3] * 100).ToString() + "\n" + "Accuracy: " + engineer.ACC_MODS[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability4.text = "RIFLE SHOT" + "\n" + "Crit: " + rifleman.CRIT_MODS[3].ToString() + "%"+"\n" + "Damage: " + (rifleman.DMG_MODS[3] * 100).ToString() + "\n" + "Accuracy: " + rifleman.ACC_MODS[3].ToString();
            }
        }


        if (ability == 4)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability5.text = "WAR CHANT" + "\n" + "Crit: " + enforcer.CRIT_MODS[4].ToString() + "%" + "\n" + "Damage: " + (enforcer.DMG_MODS[4] * 100).ToString() + "\n" + "Accuracy: " + enforcer.ACC_MODS[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability5.text = "TASER" + "\n" + "Crit: " + medic.CRIT_MODS[4].ToString() + "%"+"\n" + "Damage: " + (medic.DMG_MODS[4] * 100).ToString() + "\n" + "Accuracy: " + medic.ACC_MODS[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability5.text = "SNARE" + "\n" + "Crit: " + engineer.CRIT_MODS[4].ToString() + "%"+"\n" + "Damage: " + (engineer.DMG_MODS[4] * 100).ToString() + "\n" + "Accuracy: " + engineer.ACC_MODS[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability5.text = "SHOTGUN" + "\n" + "Crit: " + rifleman.CRIT_MODS[4].ToString() + "%"+"\n" + "Damage: " + (rifleman.DMG_MODS[4] * 100).ToString() + "\n" + "Accuracy: " + rifleman.ACC_MODS[4].ToString();
            }
        }

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
