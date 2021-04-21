using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision2D = UnityEngine.Collision2D;

public class ActivateFirstSFlowchart : MonoBehaviour
{

    public Flowchart firstFlow;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME))
        {
            firstFlow.gameObject.SetActive(true);
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME))
    //    {
    //        firstFlow.gameObject.SetActive(false);
    //    }
    //}
}
