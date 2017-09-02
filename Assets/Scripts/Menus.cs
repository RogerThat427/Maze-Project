using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    public void Pause(){
        
    }

    public void Quit(){
        Debug.Log("Application Quit");
        Application.Quit();
    }

    public void Restart(){
		Time.timeScale = 1;
		//Player.GetComponent<FirstPerson_Controller>().enabled = true;
		Screen.lockCursor = true;
		Application.LoadLevel(Application.loadedLevel);
    }
}
