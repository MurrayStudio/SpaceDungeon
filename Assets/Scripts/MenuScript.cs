using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startBtn, exitBtn;

    // Use this for initialization
    void Start () {
	
	}

    public void Play()
    {
        //SceneManager.LoadScene("Main");
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
