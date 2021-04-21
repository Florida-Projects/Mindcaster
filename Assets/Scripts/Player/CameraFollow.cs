using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Declaración del GameManager
    private GameObject gameManager;
    private GameManager managerScript;
    public Camera cam;

    #region Design parameters
    [Range(0, 1)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool cameraClamp;
    public float horizontalClRight;
    public float horizontalClLeft; 
    public float verticalClUp;
    public float verticalClDown;
    public bool globalClamp;
    public float globalClampUp;
    public float globalClampLeft;
    public float globalClampRight;
    public float globalClampDown;
    #endregion

    #region Private variables
    private Transform target;
    Vector3 mousePos;
    #endregion

    #region Public variables
    [HideInInspector]
    public bool otherTarget;
    public Transform alternativeTarget;
    public float zoomSpeed;
    public float maxZoom;
    #endregion

    void Start()
    {
        //Instancia del GameManager
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER);
        managerScript = gameManager.GetComponent<GameManager>();

        //Instancia de variables
        target = managerScript.GetPlayer().transform;

        cam = GetComponent<Camera>();
    }

    void Update()
    {
        //Prueba
        mousePos = managerScript.GetPlayer().GetComponent<PlayerMovement>().mousePos;
    }

    void FixedUpdate()
    {
        Transform selectedTransform;

        if (!otherTarget)
        {
            selectedTransform = managerScript.GetPlayer().GetComponent<Transform>();
        } else
        {
            selectedTransform = alternativeTarget;
        }

        

        //Posición de la cámara 
        Vector3 desiredPosition;

        if(!managerScript.GetPlayer().GetComponent<PlayerMovement>().isInBounds && !otherTarget)
        {
            desiredPosition = mousePos + offset;

        } else
        {
            desiredPosition = selectedTransform.position + offset;
        }

        //El método Lerp interpola las posiciones a y b en t de la siguiente manera: (b - a) * t 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        if(cameraClamp)
        {
            float clampedX = Mathf.Clamp(smoothedPosition.x, selectedTransform.position.x + horizontalClLeft, selectedTransform.position.x + horizontalClRight);
            float clampedY = Mathf.Clamp(smoothedPosition.y, selectedTransform.position.y + verticalClDown, selectedTransform.position.y + verticalClUp);
            float clampedZ = Mathf.Clamp(smoothedPosition.z, -200, 200);
            transform.position = new Vector3(clampedX, clampedY, clampedZ);
        }
        else
        {
            transform.position = smoothedPosition;
        }

        if (globalClamp)
        {
            float globalClampX = Mathf.Clamp(transform.position.x, globalClampLeft, globalClampRight);
            float globalClampY = Mathf.Clamp(transform.position.y, globalClampDown, globalClampUp);
            transform.position = new Vector3(globalClampX, globalClampY, transform.position.z);
        }//ewe
    }

    public void ZoomEvent()
    {
        if (cam.orthographicSize > maxZoom)
        {
            cam.orthographicSize -=  Time.deltaTime * zoomSpeed;
            Invoke("ZoomEvent", 0);
        } else
        {
            CancelInvoke("ZoomEvent");
        }
        
    }
}