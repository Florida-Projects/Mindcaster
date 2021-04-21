using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Collision2D = UnityEngine.Collision2D;

public class ActivateAscensor : MonoBehaviour
{
    public Flowchart AscensorFLow;
    public Flowchart changeScene;
    private PersistentManagerScript persistentManager;
    private void Start()
    {
        try { persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>(); } catch (Exception e) { }
            
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (persistentManager != null)
        {
            if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME) && persistentManager.boosIsDeath)
            {
                GetComponent<Animator>().enabled = true;
                changeScene.gameObject.SetActive(true);
            }
            else
            {
                AscensorFLow.gameObject.SetActive(true);
            }
        }

    }
   void changeCorridor() {
       
        SceneManager.LoadScene(5);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (persistentManager != null)
        {
            if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME) && persistentManager.boosIsDeath)
            {
                GetComponent<Animator>().enabled = true;
                changeScene.gameObject.SetActive(false);
            }
            else
            {
                AscensorFLow.gameObject.SetActive(false);
            }
        }
    }
}
