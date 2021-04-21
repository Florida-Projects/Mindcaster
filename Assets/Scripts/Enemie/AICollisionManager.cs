using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AICollisionManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealt;
    public int bulletDamage;
    public GameObject sprite;

    float maxScaleX, maxScaleY, maxScaleZ;
    Animator animator;
    AISkillManager skillManager;
    SFXManager sFX;

    private void Start()
    {
        sFX = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        animator = GetComponent<Animator>();
        skillManager = GetComponent<AISkillManager>();
        //Reseteo de sistema de vida
        currentHealt = maxHealth;

        maxScaleX = sprite.transform.localScale.x;
        maxScaleY = sprite.transform.localScale.y;
        maxScaleZ = sprite.transform.localScale.z;

        //transform.Find("Health Image").GetComponent<Animator>().enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_BULLET) && GetComponent<AICollisionManager>().isActiveAndEnabled)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("boss_fatiga_idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("boss_fatiga_ending"))
            {
                skillManager.playerRegeneration = false;
            }

            TakeDamage(bulletDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        //if (!sFX.bossHit.isPlaying)
        //{
        //    sFX.bossHit.Play();
        //}
        sFX.bossHit.Play();
        currentHealt -= damage;
        Vector3 newScale = new Vector3((maxScaleX * ((float)damage / maxHealth)), (maxScaleY * ((float)damage / maxHealth)), (maxScaleZ * ((float)damage / maxHealth)));

        sprite.transform.localScale -= newScale;

        if (currentHealt <= 0)
        {
            skillManager.rip = true;
        }
    }
}
