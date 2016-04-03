using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePlayController : MonoBehaviour {

	public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

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
        popUP.SetActive(false);
		popUPAttack.SetActive(false);

        //Assign health to each enemy

      
        

        //for now, just have ability character attack configs enabled in this way
        ability1EnableArray = new bool[] { true, true, false, true };
        ability2EnableArray = new bool[] { false, false, false, true };
        ability3EnableArray = new bool[] { true, true, true, true };
        ability4EnableArray = new bool[] { true, true, false, false };
        ability5EnableArray = new bool[] { true, false, false, false };
    }
	
	// Update is called once per frame
	void Update () {
	    
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
                attack(1);
                break;
            case "Enemy2":
                hidePopUpAttack();
                hidePopUp();
                attack(2);
                break;
            case "Enemy3":
                hidePopUpAttack();
                hidePopUp();
                attack(3);
                break;
            case "Enemy4":
                hidePopUpAttack();
                hidePopUp();
                attack(4);
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
                for(int i=0; i < ability1EnableArray.Length; i++)
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
                for (int i = 0; i < ability2EnableArray.Length; i++)
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
                for (int i = 0; i < ability3EnableArray.Length; i++)
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
                for (int i = 0; i < ability4EnableArray.Length; i++)
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
                for (int i = 0; i < ability5EnableArray.Length; i++)
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

    public void attack(int character)
    {
        switch (character)
        {
            case 1:
                //attack enemy character 1
                break;
            case 2:
                //attack enemy character 2
                break;
            case 3:
                //attack enemy character 3
                break;
            case 4:
                //attack enemy character 4
                break;
        }
    }
}
