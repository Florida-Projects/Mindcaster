using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{

    ParticleSystem splash;
    GameManager manager;

    void Start()
    {
        manager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        splash = manager.GetPlayer().transform.Find("Splash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySplash()
    {
        splash.Play();
    }

}
