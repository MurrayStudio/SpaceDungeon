using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayController : MonoBehaviour {

	public GameObject popUP;
    public Image popUpImage;
    public Text popUpText;

	public GameObject popUPAttack;
	public Button enemy1Attack, enemy2Attack, enemy3Attack, enemy4Attack;

    bool popUPEnabled = false;

	// Use this for initialization
	void Start () {
        popUP.SetActive(false);
		popUPAttack.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

	public void OnClicked(Button button)
	{
		print(button.name);
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
