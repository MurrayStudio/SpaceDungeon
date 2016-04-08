using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePlayController : MonoBehaviour
{
    // Rank + target constants
    public readonly int ONE = 0;
    public readonly int TWO = 1;
    public readonly int THREE = 2;
    public readonly int FOUR = 3;
    public readonly int SELF = 4;
    public readonly int ALLIES = 5;
    public readonly int ENEMIES = 6;

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
    public Button[] enemyAttackButtons = new Button[7]; //first 4 are enemy, 5 is self heal, 6 is heal other, 7 is damage all
    public Button[] abilityButtons = new Button[5];

    //what array to enable in pop up attack
    private bool[] ability1EnableArray, ability2EnableArray, ability3EnableArray, ability4EnableArray, ability5EnableArray;

    //Images for UI
    public Sprite[] enforcerAbilityImages = new Sprite[5];
    public Sprite[] medicAbilityImages = new Sprite[5];
    public Sprite[] riflemanAbilityImages = new Sprite[5];
    public Sprite[] engineerAbilityImages = new Sprite[5];
    public Sprite enabledButtonImageEnemy;
    public Sprite enabledButtonImageAlly;
    public Sprite enabledButtonImageHealAlly;
    public Sprite disabledButtonImage;

    //popUp window and components
    public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

    //holds text of current character
    public Text currentCharacterText;

    //popup attack window
    public GameObject popUPAttack;

    //popStepEnemyAttack
    public GameObject stepEnemyAttackPopUp;

    //UI pop up bools
    private bool popUPEnabled = false;
    private bool popUPAttackEnabled = false;

    //current button
    private string currentButtonClicked;

    //get coordinate info about characters on screen
    public GameObject[] characters;

    public GameObject currentCharacterArrow;


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


    // Use this for initialization
    void Start()
    {
        //init units
        enforcer = new Enforcer();
        medic = new Medic();
        rifleman = new Rifleman();
        engineer = new Engineer();

        freightEnemy1 = new Freight();
        freightEnemy2 = new Freight();
        freightEnemy2.SetStats(freightEnemy2.GetLevel(), 1, freightEnemy2.GetHealth());
        freightEnemy3 = new Freight();
        freightEnemy2.SetStats(freightEnemy3.GetLevel(), 2, freightEnemy3.GetHealth());
        freightEnemy4 = new Freight();
        freightEnemy2.SetStats(freightEnemy4.GetLevel(), 3, freightEnemy4.GetHealth());

        allies = new Unit[] { enforcer, medic, rifleman, engineer };
        enemies = new Unit[] { freightEnemy1, freightEnemy2, freightEnemy3, freightEnemy4 };

        //hardcode setup for level 1
        order = enforcer.Order(allies, enemies);

        //enforcer to start
        indexOfOrder = 0;
        currentCharacter = order[indexOfOrder];


        //popUP.SetActive(false);
        //popUPAttack.SetActive(false);
        hidePopUp();
        hidePopUpAttack();
        hideStepEnemyAttackPopUp();
    }

    // Update is called once per frame
    void Update()
    {

        currentCharacter = order[indexOfOrder];

        foreach (Unit U in order)
        {
            if (U.GetHealth() <= 0)
            {
                // TODO Remove unit here
            }
        }
		configAbilityRanges(); //every button click load up new ability array for attackingUnit[] TempArr = new Unit[allies.Length];

		//Unit[] TempArr = new Unit[4];

		//foreach (Unit U in allies)
		//{
		//	TempArr [U.GetRank ()] = U;
		//}
		//allies = TempArr;

		//foreach (Unit U in enemies)
		//{
		//	TempArr [U.GetRank ()] = U;
		//}
		//enemies = TempArr;

        currentCharacterArrow.transform.position = new Vector3(characters[indexOfOrder].transform.position.x, characters[indexOfOrder].transform.position.y + 1.5f, characters[indexOfOrder].transform.position.z);

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
            if (currentCharacter.GetCategory() == "Enforcer")
            {
                abilityButtons[0].image.sprite = enforcerAbilityImages[0];
                abilityButtons[1].image.sprite = enforcerAbilityImages[1];
                abilityButtons[2].image.sprite = enforcerAbilityImages[2];
                abilityButtons[3].image.sprite = enforcerAbilityImages[3];
                abilityButtons[4].image.sprite = enforcerAbilityImages[4];
            }
            if (currentCharacter.GetCategory() == "Medic")
            {
                abilityButtons[0].image.sprite = medicAbilityImages[0];
                abilityButtons[1].image.sprite = medicAbilityImages[1];
                abilityButtons[2].image.sprite = medicAbilityImages[2];
                abilityButtons[3].image.sprite = medicAbilityImages[3];
                abilityButtons[4].image.sprite = medicAbilityImages[4];
            }
            if (currentCharacter.GetCategory() == "Engineer")
            {
                abilityButtons[0].image.sprite = engineerAbilityImages[0];
                abilityButtons[1].image.sprite = engineerAbilityImages[1];
                abilityButtons[2].image.sprite = engineerAbilityImages[2];
                abilityButtons[3].image.sprite = engineerAbilityImages[3];
                abilityButtons[4].image.sprite = engineerAbilityImages[4];
            }
            if (currentCharacter.GetCategory() == "Rifleman")
            {
                abilityButtons[0].image.sprite = riflemanAbilityImages[0];
                abilityButtons[1].image.sprite = riflemanAbilityImages[1];
                abilityButtons[2].image.sprite = riflemanAbilityImages[2];
                abilityButtons[3].image.sprite = riflemanAbilityImages[3];
                abilityButtons[4].image.sprite = riflemanAbilityImages[4];
            }

            ability1EnableArray = currentCharacter.GetAttackRange(0);
            ability2EnableArray = currentCharacter.GetAttackRange(1);
            ability3EnableArray = currentCharacter.GetAttackRange(2);
            ability4EnableArray = currentCharacter.GetAttackRange(3);
            ability5EnableArray = currentCharacter.GetAttackRange(4);
        }

        if (currentCharacter.GetFriendly() == false)
        {

            showStepEnemyAttackPopUp(); //so we can step through enemy attacks
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
            case "selfHeal":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, allies[indexOfOrder]);
                    ++indexOfOrder;
                }
                break;
            case "healOther":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, allies[0]); //make this 0 for now
                    ++indexOfOrder;
                }
                break;
            case "allEnemy":
                hidePopUpAttack();
                hidePopUp();
                if (currentCharacter.GetFriendly() == true)
                {
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[0]);
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[1]);
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[2]);
                    currentCharacter.MakeMove(currentAbility, allies, enemies, enemies[3]);
                    ++indexOfOrder;
                }
                break;
            case "StepAttackEnemy":
                //make a simple ai move
                int move = Random.Range(0, 2);
                int allyHit = Random.Range(0, 3);
                currentCharacter.MakeMove(move, allies, enemies, allies[allyHit]);
                if (indexOfOrder < order.Length - 1)
                {
                    ++indexOfOrder;
                }
                else
                {
                    indexOfOrder = 0;
                }
                hideStepEnemyAttackPopUp();
                break;
        }
    }

    private void configAbilityRanges()
    {
        //config what buttons are enabled based off what ability selected:
        switch (currentButtonClicked)
        {
            case "ability1":
                for (int i = 0; i < 7; i++)
                {
                    if (ability1EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        if (i == 4)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageAlly; //self heal
                        }
                        else if (i == 5)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageHealAlly; //heal other
                        }
                        else
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageEnemy; //enemy
                        }
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability2":
                for (int i = 0; i < 7; i++)
                {
                    if (ability2EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        if (i == 4)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageAlly; //self heal
                        }
                        else if (i == 5)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageHealAlly; //heal other
                        }
                        else
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageEnemy; //enemy
                        }
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability3":
                for (int i = 0; i < 7; i++)
                {
                    if (ability3EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        if (i == 4)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageAlly; //self heal
                        }
                        else if (i == 5)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageHealAlly; //heal other
                        }
                        else
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageEnemy; //enemy
                        }
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability4":
                for (int i = 0; i < 7; i++)
                {
                    if (ability4EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        if (i == 4)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageAlly; //self heal
                        }
                        else if (i == 5)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageHealAlly; //heal other
                        }
                        else
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageEnemy; //enemy
                        }
                    }
                    else
                    {
                        enemyAttackButtons[i].interactable = false;
                        enemyAttackButtons[i].image.sprite = disabledButtonImage;
                    }
                }
                break;
            case "ability5":
                for (int i = 0; i < 7; i++)
                {
                    if (ability5EnableArray[i] == true)
                    {
                        enemyAttackButtons[i].interactable = true;
                        if (i == 4)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageAlly; //self heal
                        }
                        else if (i == 5)
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageHealAlly; //heal other
                        }
                        else
                        {
                            enemyAttackButtons[i].image.sprite = enabledButtonImageEnemy; //enemy
                        }
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
                ability1.text = "HEAVY SWING" + "\n" + "Crit: " + enforcer.CritMods[0].ToString() + "%\n" + "Damage: " + (100 + enforcer.DmgMods[0] * 100).ToString() + "%\n" + "Accuracy: " + enforcer.AccMods[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability1.text = "ADRENALINE" + "\n" + "Crit: " + medic.CritMods[0].ToString() + "%\n" + "Damage: " + (100 + medic.DmgMods[0] * 100).ToString() + "%\n" + "Accuracy: " + medic.AccMods[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability1.text = "FLASH BANG" + "\n" + "Crit: " + engineer.CritMods[0].ToString() + "%\n" + "Damage: " + (100 + engineer.DmgMods[0] * 100).ToString() + "%\n" + "Accuracy: " + engineer.AccMods[0].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability1.text = "BAYONET STAB" + "\n" + "Crit: " + rifleman.CritMods[0].ToString() + "%\n" + "Damage: " + (100 + rifleman.DmgMods[0] * 100).ToString() + "%\n" + "Accuracy: " + rifleman.AccMods[0].ToString();
            }
        }

        if (ability == 1)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability2.text = "KICK" + "\n" + "Crit: " + enforcer.CritMods[1].ToString() + "%\n" + "Damage: " + (100 + enforcer.DmgMods[1] * 100).ToString() + "%\n" + "Accuracy: " + enforcer.AccMods[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability2.text = "BULWARK" + "\n" + "Crit: " + medic.CritMods[1].ToString() + "%\n" + "Damage: " + (100 + medic.DmgMods[1] * 100).ToString() + "%\n" + "Accuracy: " + medic.AccMods[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability2.text = "ION PULSE" + "\n" + "Crit: " + engineer.CritMods[1].ToString() + "%\n" + "Damage: " + (100 + engineer.DmgMods[1] * 100).ToString() + "%\n" + "Accuracy: " + engineer.AccMods[1].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability2.text = "NET" + "\n" + "Crit: " + rifleman.CritMods[1].ToString() + "%\n" + "Damage: " + (100 + rifleman.DmgMods[1] * 100).ToString() + "%\n" + "Accuracy: " + rifleman.AccMods[1].ToString();
            }
        }


        if (ability == 2)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability3.text = "SLICE" + "\n" + "Crit: " + enforcer.CritMods[2].ToString() + "%\n" + "Damage: " + (100 + enforcer.DmgMods[2] * 100).ToString() + "%\n" + "Accuracy: " + enforcer.AccMods[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability3.text = "HEALING WAVE" + "\n" + "Crit: " + medic.CritMods[2].ToString() + "%\n" + "Damage: " + (100 + medic.DmgMods[2] * 100).ToString() + "%\n" + "Accuracy: " + medic.AccMods[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability3.text = "LIGHT WALL" + "\n" + "Crit: " + engineer.CritMods[2].ToString() + "%\n" + "Damage: " + (100 + engineer.DmgMods[2] * 100).ToString() + "%\n" + "Accuracy: " + engineer.AccMods[2].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability3.text = "RELOAD" + "\n" + "Crit: " + rifleman.CritMods[2].ToString() + "%\n" + "Damage: " + (100 + rifleman.DmgMods[2] * 100).ToString() + "%\n" + "Accuracy: " + rifleman.AccMods[2].ToString();
            }
        }

        if (ability == 3)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability4.text = "STEROIDS" + "\n" + "Crit: " + enforcer.CritMods[3].ToString() + "%\n" + "Damage: " + (100 + enforcer.DmgMods[3] * 100).ToString() + "%\n" + "Accuracy: " + enforcer.AccMods[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability4.text = "PISTOL SHOT" + "\n" + "Crit: " + medic.CritMods[3].ToString() + "%\n" + "Damage: " + (100 + medic.DmgMods[3] * 100).ToString() + "%\n" + "Accuracy: " + medic.AccMods[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability4.text = "RATCHET GUN" + "\n" + "Crit: " + engineer.CritMods[3].ToString() + "%\n" + "Damage: " + (100 + engineer.DmgMods[3] * 100).ToString() + "%\n" + "Accuracy: " + engineer.AccMods[3].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability4.text = "RIFLE SHOT" + "\n" + "Crit: " + rifleman.CritMods[3].ToString() + "%\n" + "Damage: " + (100 + rifleman.DmgMods[3] * 100).ToString() + "%\n" + "Accuracy: " + rifleman.AccMods[3].ToString();
            }
        }


        if (ability == 4)
        {
            if (currentCharacter.GetType().ToString() == "Enforcer")
            {
                ability5.text = "WAR CHANT" + "\n" + "Crit: " + enforcer.CritMods[4].ToString() + "%\n" + "Damage: " + (100 + enforcer.DmgMods[4] * 100).ToString() + "%\n" + "Accuracy: " + enforcer.AccMods[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Medic")
            {
                ability5.text = "TASER" + "\n" + "Crit: " + medic.CritMods[4].ToString() + "%\n" + "Damage: " + (100 + medic.DmgMods[4] * 100).ToString() + "%\n" + "Accuracy: " + medic.AccMods[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Engineer")
            {
                ability5.text = "SNARE" + "\n" + "Crit: " + engineer.CritMods[4].ToString() + "%\n" + "Damage: " + (100 + engineer.DmgMods[4] * 100).ToString() + "%\n" + "Accuracy: " + engineer.AccMods[4].ToString();
            }
            if (currentCharacter.GetType().ToString() == "Rifleman")
            {
                ability5.text = "SHOTGUN" + "\n" + "Crit: " + rifleman.CritMods[4].ToString() + "%\n" + "Damage: " + (100 + rifleman.DmgMods[4] * 100).ToString() + "%\n" + "Accuracy: " + rifleman.AccMods[4].ToString();
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

    public void hideStepEnemyAttackPopUp()
    {
        //stepEnemyAttackPopUp.SetActive(false);
    }

    public void showStepEnemyAttackPopUp()
    {
        stepEnemyAttackPopUp.SetActive(true);
    }
}
