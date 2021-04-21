using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AISkillManager : MonoBehaviour
{
    public float maxTimeBetweenSkills;
    public float minTimeBetweenSkills;
    public float timeBetweenStages;
    public float fatigaEndingDuration;
    public float timer;
    public float currentTime;
    float regenerationTimer;

    public bool firstStage = true;
    public bool rip;

    public List<string> skillNames;
    public List<int> index = new List<int>();
    public float regenerationRate;
    private PersistentManagerScript persistentManager;
    public string currentSkill = "BurstSkill";
    AICollisionManager collisionManager;
    GameManager manager;
    BurstSkill burstSkill;
    Animator animator;
    float defaultShakeDuration;
    bool virgin = true;
    bool virgin2 = true;
    public bool playerRegeneration;
    public int playerRegenerationValue;
    EgocentrismSkill egocentrismSkill;
    AnimatorOverrideController animatorOverrideController;
    public Flowchart flowFatiga;
    public Flowchart muerte;
    public AnimationClip bossIdle;
    int i = 0;
    SFXManager sFXManager;
    float damageTimer;
    float damageTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>();
        }
        catch (Exception e) { 
        
        }
        sFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        egocentrismSkill = GetComponent<EgocentrismSkill>();
        collisionManager = GetComponent<AICollisionManager>();
        burstSkill = GetComponent<BurstSkill>();
        manager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        fillIndexList();
        UseSkill(currentSkill);
        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;

        if (firstStage)
        {
            currentTime += Time.deltaTime;

            if (currentTime > timer && firstStage)
            {
                Alternate();
                SetTimer();
            }
        }
        else
        {
            animatorOverrideController["boss_idle"] = bossIdle;

            if (virgin2)
            {
                
                egocentrismSkill.DestroyAllBullets();
                virgin2 = false;
            }
        }


        if (Vector3.Distance(manager.GetPlayer().transform.position, transform.position) < 2 && !rip)
        {
            if (damageTimer > damageTime)
            {
                manager.GetPlayer().GetComponent<PlayerMovement>().TakeDamage(5);
                damageTimer = 0;
            }
        } 
        

        if (!rip)
        {
            if (gameObject.GetComponent<AICollisionManager>().currentHealt < gameObject.GetComponent<AICollisionManager>().maxHealth / 1.33f)
            {
                firstStage = false;
                if (virgin)
                {
                    burstSkill.timeBetweenShoots = 0.4f;
                    burstSkill.bulletForce = 25;
                    if (!persistentManager.fatigaTutorialHasFinished) {
                        flowFatiga.gameObject.SetActive(true);
                        persistentManager.fatigaTutorialHasFinished = true;
                    }
                   
                    animator.SetTrigger("fatigaToIntro");
                    DisableAllSkills();
                    virgin = false;
                    playerRegeneration = true;
                    StartCoroutine(ExecFatigaEnding());
                    timer = 0;

                }
                Invoke("SecondStage", timeBetweenStages);
            }
        }
        else
        {
            DisableAllSkills();
            collisionManager.enabled = false;           
            if (persistentManager != null) {
                persistentManager.boosIsDeath = true;
            }           
            animator.SetTrigger("muerte");
            egocentrismSkill.DestroyAllBullets();
            muerte.gameObject.SetActive(true);
            muerte.SetBooleanVariable("bossDefeat", true);
        }


        

        if (playerRegeneration)
        {
            regenerationTimer += Time.deltaTime;

            if (regenerationTimer >= regenerationRate)
            {
                manager.GetPlayer().GetComponent<PlayerMovement>().HealthRegeneration(playerRegenerationValue);
                regenerationTimer = 0;
            }
            
        }
    }

    void SetTimer()
    {
        timer = UnityEngine.Random.Range(minTimeBetweenSkills, maxTimeBetweenSkills + 1);
        currentTime = 0;
    }


    void SecondStage()
    {
        manager.GetMainCamera().GetComponent<CameraShake>().shakeTime = defaultShakeDuration;
        
        currentTime += Time.deltaTime;
        int randomSkill = 0;

        if (currentTime > timer)
        {
            if (i > 3) { i = 0; }
                
            randomSkill = UnityEngine.Random.Range(0, index.Count);
            index.Remove(randomSkill);

            UseSkill(skillNames[i]);
            i++;
            SetTimer();
        }

        timeBetweenStages = 0;
    }

    IEnumerator ExecFatigaEnding()
    {
        yield return new WaitForSeconds(timeBetweenStages-fatigaEndingDuration);
        animator.SetTrigger("fatigaToEnding");
        defaultShakeDuration = manager.GetMainCamera().GetComponent<CameraShake>().shakeTime;
        manager.GetMainCamera().GetComponent<CameraShake>().shakeTime = fatigaEndingDuration;
        manager.GetMainCamera().GetComponent<CameraShake>().ShakeIt();
        sFXManager.bossCries.Play();
        playerRegeneration = false;
    }

    void fillIndexList()
    {
        index = new List<int>();
        for (int i = 0; i < skillNames.Count; i++) { index.Add(i); }
    }

    void Alternate()
    {
        if (currentSkill.Equals("BurstSkill"))
        {
            UseSkill("EgocentrismSkill");
        } 
        
        else
        {
            UseSkill("BurstSkill");
        }
    }

    public void UseSkill(string typeOf)
    {
        foreach (string skill in skillNames)
        {
            if (skill.Equals(typeOf))
            {
                (gameObject.GetComponent(skill) as MonoBehaviour).enabled = true;
            } else
            {
                (gameObject.GetComponent(skill) as MonoBehaviour).enabled = false;
            }
        }

        currentSkill = typeOf;
    }

    public void DisableAllSkills()
    {
        foreach (string skill in skillNames)
        {
            (gameObject.GetComponent(skill) as MonoBehaviour).enabled = false;
        }
    }
}
