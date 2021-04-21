using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(Globals.GO_PLAYER_NAME))
        {
            if (SceneManager.GetActiveScene().name.Equals("Hotel"))
            {
                SceneManager.LoadScene(2);
            } else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
