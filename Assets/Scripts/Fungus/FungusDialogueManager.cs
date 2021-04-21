using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusDialogueManager : MonoBehaviour
{

    
    #region PlayerVariables
    private PlayerMovement movement;
    #endregion   
    private Animator mainCharaterAnimator;
    private GameManager gm;
    private PersistentManagerScript persistentManager;



    // Start is called before the first frame update
    void Start()
    {
        //Inicializacion de los scripts a interactuar.
        gm = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        movement = gm.GetPlayer().GetComponent<PlayerMovement>();
        Debug.Log(movement.currentHealt);
        mainCharaterAnimator = gm.GetPlayer().GetComponent<Animator>();
        try { 
            persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>(); 
        } catch (System.Exception e) {
           
        }
       
    }

  
    void disableScripts() {
        movement.enabled = false;
        mainCharaterAnimator.enabled = false;
       
    }

    void activateScripts() {
        movement.enabled = true;
        mainCharaterAnimator.enabled = true;

    }
    void addtoPersistent() {
        if (persistentManager != null) {
            persistentManager.talkedToTravis = true;
        }
    }
}
