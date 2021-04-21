using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public float maxTimeBetweenSplash;
    public float minTimeBetweenSplash;

    float timer;
    float timeBetweenSplash;

    public List<GameObject> drops;

    private void Start()
    {
        timeBetweenSplash = 0.5f;
        //hi
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenSplash)
        {
            int randomIndex = Random.Range(0, drops.Count);
            drops[randomIndex].GetComponent<ParticleSystem>().Play();
            RandomTime();
            timer = 0;
        }
    }

    void RandomTime()
    {
        timeBetweenSplash = Random.Range(minTimeBetweenSplash, maxTimeBetweenSplash);
    }


}
