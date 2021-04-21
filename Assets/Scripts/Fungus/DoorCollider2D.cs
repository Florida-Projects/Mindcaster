using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCollider2D : MonoBehaviour
{
    public Flowchart flowCollision;
    public Flowchart checkToPass;
    public Flowchart changeSceneFlow;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL) && !checkToPass.GetBooleanVariable("changedClothes")) {
            flowCollision.gameObject.SetActive(true);
        } else if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL) && checkToPass.GetBooleanVariable("changedClothes")) {
            changeSceneFlow.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
        {
            flowCollision.gameObject.SetActive(false);
        }
    }
    void changeScene() {
        SceneManager.LoadScene(3);
    }
}

