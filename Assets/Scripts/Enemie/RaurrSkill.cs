using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaurrSkill : MonoBehaviour
{
    float timer;
    float totalDuration = 6.50f;
    Animator animator;
    public GameObject handPoint;
    AISkillManager aimanager;
    GameManager gameManager;
    public float handDistance;
    bool virgin = true;
    bool virgin2 = true;
    float damageTimer;
    public float damageTime;
    float defaultShakeTime;
    public GameObject yellowPrefab;
    public GameObject redPrefab;
    public List<Transform> positions;
    SFXManager sFX;
    public float fireRate;
    float fireTimer;

    private void OnEnable()
    {
        sFX = GameObject.Find("SFXManager").GetComponent<SFXManager>();    
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        aimanager = GetComponent<AISkillManager>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("HandAttack");
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        fireTimer += Time.deltaTime;

        if (timer > totalDuration)
        {
            aimanager.currentTime = 200000;
        }

        if (timer > 5 && virgin2)
        {
            sFX.raurr.Play();
            virgin2 = false;
        }

        if (timer > 5.5f)
        {
            defaultShakeTime = gameManager.GetMainCamera().GetComponent<CameraShake>().shakeTime;
            gameManager.GetMainCamera().GetComponent<CameraShake>().shakeTime = 0.38f;
            gameManager.GetMainCamera().GetComponent<CameraShake>().ShakeIt();
            
            gameManager.GetMainCamera().GetComponent<CameraShake>().shakeTime = defaultShakeTime;
            
            if (fireTimer > fireRate)
            {
                InstantiateBalls();
                fireTimer = 0;
            }  

            if (Vector3.Distance(gameManager.GetPlayer().transform.position, handPoint.transform.position) <= handDistance)
            {
                if (virgin)
                {
                    gameManager.GetPlayer().GetComponent<PlayerMovement>().TakeDamage(30);
                }

                else
                {
                    gameManager.GetPlayer().GetComponent<PlayerMovement>().TakeDamage(1);
                }
            }             
        }
    }

    void InstantiateBalls()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject selectedBullet = Random.Range(0, 101) >= 50 ? redPrefab : yellowPrefab;
            GameObject bullet = Instantiate(selectedBullet, positions[i].position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 force = positions[i].up * 20;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnDisable()
    {
        virgin2 = true;
    }

}


