using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level01");
    }

    public void Quit()
    {
        print("Quitting");
        Application.Quit();
    }
}
