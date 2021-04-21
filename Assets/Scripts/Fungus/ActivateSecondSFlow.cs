using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Collision2D = UnityEngine.Collision2D;

public class ActivateSecondSFlow : MonoBehaviour
{
 public Flowchart changeFlow;
 public Flowchart firstFlow;
 public Flowchart thirdFlow;
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME) && firstFlow.GetBooleanVariable("hasTalk"))
       {
                changeFlow.gameObject.SetActive(true);
                firstFlow.gameObject.SetActive(false);
          
        }
        
      if (changeFlow.GetBooleanVariable("ChangedClothes"))
            {
                changeFlow.gameObject.SetActive(false);
                thirdFlow.gameObject.SetActive(true);
            }

        }
      
        
        
    
}
