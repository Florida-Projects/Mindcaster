using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialDialogChangeImages : MonoBehaviour
{
    public Image backgroundImage;
    public Image novaImage;
    public AudioSource audio;
    public AudioClip NOVAMUSIC;
    private void Start()
    {
        novaImage.color = Color.black;
    }
 
    void changeSecondImage() {
       
        novaImage.gameObject.SetActive(true);       
        //NOVA = 119, 192, 255, 255 A  (77C0FF)
        novaImage.color = new Vector4(0.466f, 0.753f, 1f, 1f);       
        novaImage.GetComponent<Animator>().enabled = true;
        audio.Stop();
        audio.clip = NOVAMUSIC;
        audio.Play();
    }
}
