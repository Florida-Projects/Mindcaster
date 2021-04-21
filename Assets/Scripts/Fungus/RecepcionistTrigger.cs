using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecepcionistTrigger : MonoBehaviour
{
    public Flowchart flow;
    public Flowchart flow2;
    private PersistentManagerScript persistentManager;

    private void Start()
    {
        persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!persistentManager.boosIsDeath)
        {
            if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
            {
                flow.SetBooleanVariable(Globals.FUNGUS_BOOL_ABLE_TALK_RECEPCIONIST, true);
            }
        }
        else {
            flow2.gameObject.SetActive(true);
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!persistentManager.boosIsDeath)
        {
            if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
            {
                flow.SetBooleanVariable(Globals.FUNGUS_BOOL_ABLE_TALK_RECEPCIONIST, false);
            }
        }
        else {
            flow2.gameObject.SetActive(false);
        }
    }
}
