using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    public float maxHealth;
    public float lifeTime;
    public float maxLifeTime;
    public float bulletDamageForMirror;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_BULLET))
        {
            currentHealth -= bulletDamageForMirror;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
