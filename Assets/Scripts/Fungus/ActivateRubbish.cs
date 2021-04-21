using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRubbish : MonoBehaviour
{
    public Flowchart flowCollision;
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL)) {
            flowCollision.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
        {
            flowCollision.gameObject.SetActive(false);
        }
    }
}
