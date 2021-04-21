using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void FungusChangeScene() {

        switch(SceneManager.GetActiveScene().name){
            case Globals.SCENE_EXT_HOTEL:
                    SceneManager.LoadScene(3);
                break;
            case Globals.SCENE_RECEPCION:
                 SceneManager.LoadScene(4);
                break;
            case Globals.SCENE_BOSS_FIGHT:
                SceneManager.LoadScene(3);
                break;
           

        }
    
    }
}
