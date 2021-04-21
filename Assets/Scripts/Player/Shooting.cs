using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Declaración del GameManager
    private GameObject gameManager;
    private GameManager managerScript;
    SFXManager sFXManager;

    #region Design parameters
    public float bulletForce = 20f;
    public float shootDelay;
    public int bullets;
    #endregion

    #region Public variables
    [HideInInspector]
    public Transform gun;
    public Transform gunAxis;
    public GameObject bulletPrefab;  
    #endregion

    #region Private variables
    float shootTimer;
    CameraShake shake;
    #endregion

    void Start()
    {
        //Instancia del GameManager
        sFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER);
        managerScript = gameManager.GetComponent<GameManager>();
        gun = managerScript.GetGun().transform;
        gunAxis = managerScript.GetGunAxis().transform;
        shake = managerScript.GetMainCamera().GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetAxis("Fire1") > 0 && bullets > 0 && shootTimer >= shootDelay)
        {
            Shoot();
            shootTimer = 0;
        }
    }


    void Shoot()
    {
        sFXManager.playerShoot.Play();
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gunAxis.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gunAxis.up * bulletForce, ForceMode2D.Impulse);
        bullets--;
    }
}
