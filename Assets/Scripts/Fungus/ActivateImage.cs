using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateImage : MonoBehaviour
{
    public Image imageBlack;
    public AudioSource audio;
    void activateImageBlack() {
        imageBlack.gameObject.SetActive(true);
        audio.Stop();
    }
}
