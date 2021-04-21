using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowDoors : MonoBehaviour
{
    public Flowchart doorFlow;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME))
            doorFlow.gameObject.SetActive(true);
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME))
             doorFlow.gameObject.SetActive(true);
    }
}
