﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;


    public void PauseGame()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

   public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void BackToMainMenu()
    {
        if (gameIsPaused) {
            ResumeGame();        
        }
        SceneManager.LoadScene(0);
    }
   



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) { 
                ResumeGame(); 
            }             
            else { 
                PauseGame(); 
            } 
                            
        }
    }
}
