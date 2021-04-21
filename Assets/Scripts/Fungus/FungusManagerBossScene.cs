using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusManagerBossScene : MonoBehaviour
{
    public GameObject enemy;
    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {

      
    }

    void activateGamePlay()
    {       
      enemy.GetComponent<AISkillManager>().enabled = true;
      mainCharacter.GetComponent<PlayerMovement>().enabled = true;
      mainCharacter.GetComponent<Shooting>().enabled = true;
      mainCharacter.GetComponent<PlayerSkillManager>().enabled = true;
           
    }   
    void disableGamePlay()
    {
       
       enemy.GetComponent<AISkillManager>().enabled = false;
       mainCharacter.GetComponent<PlayerMovement>().enabled = false;
       mainCharacter.GetComponent<Shooting>().enabled = false;
       mainCharacter.GetComponent<PlayerSkillManager>().enabled = false;
      
    }

}
