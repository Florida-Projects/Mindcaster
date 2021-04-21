using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using System;

//Comentario de prueba
public class PlayerMovement : MonoBehaviour
{
    public Flowchart flowEnd;
    //Declaración del GameManager
    private GameObject gameManager;
    private GameManager managerScript;
    SFXManager sFXManager;
    //Declaración del VFXManager
    VFXManager vFXManager;
    private PersistentManagerScript persistentManager;

    #region Design parameters
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealt;
    public Vector2 spawnPoint = Vector2.zero;
    public HealthBarController healtBar;
    public float timeToSigh;
    public GameObject sprite;
    #endregion

    #region Public variables
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector2 mousePos;
    [HideInInspector]
    public Bounds bounds;
    [HideInInspector]
    public bool isInBounds;
    [HideInInspector]
    public Vector2 lookDir;
    #endregion

    #region Private variables
    Vector2 movement;
    float clampedDirX;
    float clampedDirY;
    float auxX = 0.0f;
    float auxY = 0.0f;
    GameObject gunAxis;
    bool gunFlip;
    public float sighTimer;
    float maxScaleX, maxScaleY, maxScaleZ;
    #endregion

    void Start()
    {
        try
        {
            persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>();
        }
        catch (Exception e)
        {
       
        }

        try
        {
            vFXManager = GameObject.Find("VFXManager").GetComponent<VFXManager>();
            sFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        }
        catch (Exception ex){
           
        }


     //Instancia del GameManager
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER);
        //Recogemos el componente GameManager
        managerScript = gameManager.GetComponent<GameManager>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gunAxis = managerScript.GetGunAxis();

        //Reseteo de sistema de vida
        currentHealt = maxHealth;
        if (healtBar)
        {
            healtBar.SetMaxHealth(maxHealth);
        }
        try
        {
            maxScaleX = sprite.transform.localScale.x;
            maxScaleY = sprite.transform.localScale.y;
            maxScaleZ = sprite.transform.localScale.z;
        }
        catch (Exception exc){
           
        }

    }

    void Update()
    {      
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (auxX != Input.mousePosition.x || auxY != Input.mousePosition.y)
        {
            mousePos = managerScript.GetMainCamera().GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }

        isInBounds = managerScript.GetBounds().GetComponent<Collider2D>().bounds.Contains(mousePos);

        //Asignación de las animaciones
        if (SceneManager.sceneCountInBuildSettings != 4)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);

            }
            else
            {
                animator.SetFloat("Horizontal", clampedDirX);
                animator.SetFloat("Vertical", clampedDirY);


            }
        }


        animator.SetFloat("Speed", movement.sqrMagnitude);
        auxX = Input.mousePosition.x;
        auxY = Input.mousePosition.y;


        if (gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
        {
            if (movement.sqrMagnitude == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName(Globals.ANIM_P0_SIGH))
            {
                sighTimer += Time.deltaTime;

                if (sighTimer >= timeToSigh)
                {
                    animator.SetBool("Waiting", true);
                    sighTimer = 0;
                }

            }

            if (movement.sqrMagnitude != 0 && animator.GetCurrentAnimatorStateInfo(0).IsName(Globals.ANIM_P0_SIGH))
            {
                animator.SetBool("Waiting", false);
            }
        }
     
    }

    public void HealthRegeneration(int regenration)
    {
        int health = currentHealt + regenration;
        int calmpedHealth = Mathf.Clamp(health, 0, 100);
        currentHealt = calmpedHealth;
        Vector3 newScale = new Vector3((maxScaleX * ((float)regenration / maxHealth)), (maxScaleY * ((float)regenration / maxHealth)), (maxScaleZ * ((float)regenration / maxHealth)));

        Vector3 finalScale = sprite.transform.localScale + newScale;

        float scalex = Mathf.Clamp(finalScale.x, 0, 1);
        float scaley = Mathf.Clamp(finalScale.y, 0, 1);
        float scalez = Mathf.Clamp(finalScale.z, 0, 1);

        sprite.transform.localScale = new Vector3(scalex, scaley, scalez);
    }

    void FixedUpdate()
    {
       
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            lookDir = mousePos - rb.position;
            clampedDirX = Mathf.Clamp(lookDir.x, -1, 1);
            clampedDirY = Mathf.Clamp(lookDir.y, -1, 1);

            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            //Weapon flipping
            //gunFlip = Utilities.IsBetween(angle, -90, 90) ? false : true;

            //if (managerScript.GetGun().GetComponent<SpriteRenderer>() != null)
            //{
            //    managerScript.GetGun().GetComponent<SpriteRenderer>().flipY = gunFlip;
            //}   

            //Weapon rotation
            try
            {
                gunAxis.transform.Rotate(transform.position);
                gunAxis.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            }
            catch (System.NullReferenceException nre)
            {
                  
            }
        
    }

    public void TakeDamage(int damage)
    {
        if (sFXManager != null)
        {
            if (!sFXManager.dmg.isPlaying)
            {
                sFXManager.dmg.Play();
            }

        }
        vFXManager.PlaySplash();
        managerScript.GetMainCamera().GetComponent<CameraShake>().ShakeIt();
        currentHealt -= damage;
        healtBar.SetHealth(currentHealt);

        Vector3 newScale = new Vector3((maxScaleX * ((float)damage / maxHealth)), (maxScaleY * ((float)damage / maxHealth)), (maxScaleZ * ((float)damage / maxHealth)));

        sprite.transform.localScale -= newScale;

        if (currentHealt <= 0)
        {
            managerScript.DeathEvent();
            flowEnd.SetBooleanVariable("bossDefeat", false);
            if (persistentManager != null)
            {
                persistentManager.boosIsDeath = false;
            }

            //managerScript.PlayerKill();
            //managerScript.PlayerSpawn(spawnPoint);
        }

    }



    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_ENEMY_BULLET))
        {
            TakeDamage(5);
        }
    }

}
