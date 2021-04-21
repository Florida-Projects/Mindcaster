using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Esta clase servirá para recuperar instancias del resto de GameObjects del juego
 * y contener los métodos que puedan afectar al mundo o utilizarse en mas de un lugar.*/
public class GameManager : MonoBehaviour
{
    #region GameObjects
    private GameObject player;
    private GameObject gunAxis;
    private GameObject gun;
    private GameObject mainCamera;
    private GameObject bounds;
    #endregion

    //Mouse config
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;


    public float timeToHotel;

    float defaultFixedDeltaTime;
    bool dead;

    void Awake()
    {
        #region GameObjects instances
        player = GameObject.Find(Globals.GO_PLAYER_NAME);       
        mainCamera = GameObject.Find(Globals.GO_MAIN_CAMERA);
        gunAxis = GameObject.Find(Globals.GO_GUN_AXIS);
        gun = GameObject.Find(Globals.GO_GUN);
        bounds = GameObject.Find(Globals.GO_BOUNDS);

        


        #endregion

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    #region Getters and Setters
    public GameObject GetPlayer()
    {
        return player;
    }
    public GameObject GetGunAxis()
    {
        
        return gunAxis;
    }
    public GameObject GetGun()
    {

        return gun;
    }
    public GameObject GetMainCamera()
    {
        return mainCamera;
    }
    public GameObject GetBounds()
    {
        return bounds;
    }
    #endregion

    #region Methods

    public void PlayerKill()
    {
        player.SetActive(false);
    }

    public void PlayerSpawn(Vector2 position)
    {
        player.transform.position = position;
        player.SetActive(true);
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        playerController.currentHealt = playerController.maxHealth;
        playerController.healtBar.SetMaxHealth(playerController.maxHealth);
        playerController.sprite.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DeathEvent()
    {
        if (!dead)
        {
            GetMainCamera().GetComponent<CameraShake>().shakeTime = 0;
            GetMainCamera().GetComponent<CameraShake>().shakeMagnetude = 0;
            GetPlayer().GetComponent<Animator>().SetBool("die", true);
            dead = true;
            CameraFollow cameraFollow = GetMainCamera().GetComponent<CameraFollow>();
            cameraFollow.enabled = false;
            GetPlayer().GetComponent<PlayerMovement>().enabled = false;
            ParticleSystem splash = GetPlayer().transform.Find("Splash").GetComponent<ParticleSystem>();
            var main = splash.main;
            main.loop = true;
            //main.duration = 0.5f;
            splash.Play();
            GetPlayer().transform.Find("Gun Axis").gameObject.SetActive(false);
            GetPlayer().transform.Find("GUI").GetComponent<Canvas>().enabled = false;
            GetPlayer().GetComponent<Shooting>().enabled = false;
            GetMainCamera().GetComponent<CameraShake>().cameraInitialPosition = player.transform.position + Vector3.forward * -30;
            GetMainCamera().transform.position = player.transform.position + Vector3.forward * -30;
            cameraFollow.ZoomEvent();
            Time.timeScale = 0.1f;
            defaultFixedDeltaTime = Time.fixedDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            StartCoroutine(BackToHotel());
        }
               
    }

    IEnumerator BackToHotel()
    {
        yield return new WaitForSeconds(timeToHotel);
        SceneManager.LoadScene("Hotel");
        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
    }


    #endregion
}
