using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSkill : MonoBehaviour
{
    public GameObject RedBullet;
    public GameObject YellowBullet;
    public GameObject firePoint;

    public float timeBetweenShoots;
    public float bulletForce;
    
    Vector3 force;
    Rigidbody2D rb;
    bool started;

    Animator animator;
    bool firstStage;
    AISkillManager skillManager;
    SFXManager sFX;
    bool virgin = true;

    void Start()
    {
        sFX = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        skillManager = GetComponent<AISkillManager>();
        rb = gameObject.transform.Find("Weapon").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Shoot()
    {
        sFX.shoot.Play();
        GameObject selectedBullet;
        selectedBullet = Random.Range(0, 101) <= 30 ? RedBullet : YellowBullet;
        GameObject bullet = Instantiate(selectedBullet, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        force = rb.transform.up * bulletForce;
        rbBullet.AddForce(force, ForceMode2D.Impulse);
    }


    public void CancelCurrent()
    {
        CancelInvoke("Shoot");
    }

    public void Restart()
    {
        InvokeRepeating("Shoot", 0f, timeBetweenShoots);
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        skillManager = GetComponent<AISkillManager>();
        firstStage = skillManager.firstStage;
        StartCoroutine(StartBurst());

        if (firstStage)
        {
            animator.SetTrigger("rafagaToIntro");
        } 
        
        else
        {
            animator.SetTrigger("rafaga2ToIntro");
        }

        
    }

    private void Update()
    {
        if (started && virgin)
        {
            Restart();
            virgin = false;
        }
    }

    private void OnDisable()
    {
        CancelCurrent();

        if (firstStage)
        {
            animator.SetTrigger("rafagaToEnding");
        }

        else
        {
            animator.SetTrigger("rafaga2ToEnding");
        }
        started = false;
        virgin = true;
    }

    IEnumerator StartBurst()
    {
        yield return new WaitForSeconds(0.36f);
        started = true;
    }

}
