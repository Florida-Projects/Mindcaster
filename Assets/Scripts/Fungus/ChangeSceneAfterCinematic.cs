using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterCinematic : MonoBehaviour
{
    void changeScene() {
        SceneManager.LoadScene(2);
    }
}
