using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip intro;
    public AudioClip mainTheme;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayIntro();
        StartCoroutine(FinishIntro());
    }

    IEnumerator FinishIntro()
    {
        yield return new WaitForSeconds(intro.length);
        PlayMainTheme();
        Loop();
    }

    public void PlayIntro()
    {
        audioSource.clip = intro;
        audioSource.Play();
    }

    public void PlayMainTheme()
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Loop()
    {
        audioSource.loop = true;
    }

    public void UnLoop()
    {
        audioSource.loop = false;
    }
}
