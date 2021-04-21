using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    public ReflectionSkillController reflectionSkill;
    public Shooting shootingScript;
    public float reflectionSkillMaxValue;
    public float reflectionSkillCooldown;
    public GameObject reflectionFrame;
    public GameObject mirrorPrefab;
    public Transform gunAxis;
    public GameObject mirrorPreview;
    public GameObject accumulationSkillPrefab;

    bool canReflect;
    bool canDecrease;
    bool isReflectionSelected;

    //Declaración del GameManager
    private GameObject gameManager;
    private GameManager managerScript;
    private PlayerMovement playerMovement;

    GameObject preview;
    GameObject accumulation;
    public GameObject accumulationSkillFrame;

    public float maxEnergy;
    public float currentEnergy;
    public EnergyBarController energyBarController;
    public float energyBarCooldown;


    private void Start()
    {
        reflectionFrame.SetActive(false);
        reflectionSkill.SetMaxCooldown(reflectionSkillMaxValue);
        canReflect = true;

        //Instancia del GameManager
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER);
        managerScript = gameManager.GetComponent<GameManager>();
        playerMovement = managerScript.GetPlayer().GetComponent<PlayerMovement>();

        energyBarController.SetMaxEnergy(maxEnergy);
        currentEnergy = maxEnergy;
        accumulationSkillFrame.SetActive(false);
    }

    void Update()
    {
      if (!PauseMenuManager.gameIsPaused)
        {
            bool reflectionSkillInput = Input.GetMouseButtonDown(1);
            bool mouseLeftClickInput = Input.GetMouseButtonDown(0);
            bool accumulationSkillInput = Input.GetKeyDown(KeyCode.LeftShift);

            if (accumulationSkillInput)
            {
                accumulation = Instantiate(accumulationSkillPrefab, managerScript.GetPlayer().transform.position, Quaternion.identity);
                accumulation.transform.parent = managerScript.GetPlayer().transform;
                accumulationSkillFrame.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Destroy(accumulation);
                accumulationSkillFrame.SetActive(false);
            }

            if (reflectionSkillInput && canReflect)
            {

                isReflectionSelected = !isReflectionSelected;
                reflectionFrame.SetActive(isReflectionSelected);
                shootingScript.enabled = !isReflectionSelected;
                if (preview == null)
                {
                    preview = Instantiate(mirrorPreview, playerMovement.mousePos, gunAxis.rotation);
                }
                else
                {
                    Destroy(preview);
                }
            }

            if (mouseLeftClickInput && isReflectionSelected && currentEnergy > maxEnergy / 3)
            {
                Destroy(preview);
                currentEnergy -= maxEnergy / 3;
                energyBarController.SetEnergy(currentEnergy);
                startReflection();
            }

            if (canDecrease)
            {
                reflectionSkill.DecreaseCooldown(reflectionSkillMaxValue / reflectionSkillCooldown * Time.deltaTime);
            }

            if (preview != null)
            {
                preview.transform.position = playerMovement.mousePos;
                preview.transform.rotation = gunAxis.rotation;
            }

            if (currentEnergy < maxEnergy)
            {
                float energyIncreased = maxEnergy / energyBarCooldown * Time.deltaTime;
                energyBarController.IncreaseEnergy(energyIncreased);
                currentEnergy += energyIncreased;
            }

            if (accumulation != null)
            {
                float energyDecreased = maxEnergy / energyBarCooldown * Time.deltaTime * 2;
                energyBarController.DecreaseEnergy(energyDecreased);
                currentEnergy -= energyDecreased;
            }

            if (currentEnergy < 0)
            {
                Destroy(accumulation);
                accumulationSkillFrame.SetActive(false);
            }
        }
        
    }

    void startReflection()
    {
        Instantiate(mirrorPrefab, playerMovement.mousePos, gunAxis.rotation);
        reflectionSkill.SetCooldown(reflectionSkillMaxValue);
        canReflect = false;
        canDecrease = true;
        StartCoroutine("ResetReflection");
    }

    IEnumerator ResetReflection()
    {
        isReflectionSelected = !isReflectionSelected;
        reflectionFrame.SetActive(isReflectionSelected);
        Invoke("ShootingScriptState", 0.2f);
        yield return new WaitForSeconds(reflectionSkillCooldown);
        canDecrease = false;
        canReflect = true;
    }

    //Esto es como poner una tirita en una herida de bala, pero funciona.
    void ShootingScriptState()
    {
        shootingScript.enabled = !isReflectionSelected;
    }

    
}
