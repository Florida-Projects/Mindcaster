using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Fungus.Flowchart flow; 
    public void StartGame()
    {
        flow.gameObject.SetActive(true);
      //  SceneManager.LoadScene(1);
    }

    public void Options()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
