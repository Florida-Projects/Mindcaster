using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    float counter;
    float dist;
    RaycastHit2D [] hits;
    Transform lineCastEnd;
    Transform rotator;
    GameManager manager;

    public Transform origin;
    public Transform destination;

    public float moveToCenterSpeed;
    public float degreesXSecond;
    public float lineDrawSpeed;
    GameObject particles;
    public float laserWidth0;
    public float laserWidth1;

    public Vector3 startPosition;

    public int maxAngle;
    public int minAngle;

    private int defaultLinePosition;
    bool virgin = true;
    bool virgin2 = true;
    bool start;
    float mirrorTimer;
    float mirrorLimit = 0.5f;

    Animator animator;
    public float animationEntryTime;
    SFXManager sFX;


    // Start is called before the first frame update
    void Awake()
    { 
        animator = GetComponent<Animator>();
        lineRenderer = gameObject.transform.Find("LineRenderer").GetComponent<LineRenderer>();        
        lineCastEnd = GameObject.Find("LineCast").transform;
        rotator = origin.Find("LineCastRotator").transform;
        particles = GameObject.Find("LaserBeamParticles");
        particles.SetActive(false);
        rotator.eulerAngles = new Vector3(0, 0, minAngle);
        defaultLinePosition = lineRenderer.positionCount;
        sFX = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    private void Start()
    {
        manager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        mirrorTimer += Time.deltaTime;

        if (start)
        {
            if (origin.position != startPosition)
            {
                origin.position = Vector3.MoveTowards(origin.position, startPosition, moveToCenterSpeed * Time.deltaTime);
            }
            else
            {
                if (virgin2)
                {
                    sFX.laser.Play();
                    virgin2 = false;
                }
                LaserBeam();
            }
        }
       
    }

    IEnumerator ExecLaser()
    {
        yield return new WaitForSeconds(animationEntryTime);
        start = true;
        
    }

    void LaserBeam()
    {
        
        particles.SetActive(true);

        rotator.Rotate(0, 0, -degreesXSecond * Time.deltaTime);

        float clampedRotation = rotator.localEulerAngles.z; 

        //if (Mathf.Round(rotator.localEulerAngles.z) == maxAngle || Mathf.Round(rotator.localEulerAngles.z) == minAngle+1)
        //{
        //    //clampedRotation = minAngle;
        //    degreesXSecond *= -1;
        //} 
        

        rotator.localEulerAngles = new Vector3(0, 0, clampedRotation);

        hits = Physics2D.LinecastAll(origin.position, lineCastEnd.position);
        Vector3 hitObject = lineCastEnd.position;
        

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.name.Equals("Player"))
            {
                hitObject = hit.collider.transform.position;
                manager.GetPlayer().GetComponent<PlayerMovement>().TakeDamage(1);
            }

            else if (hit.collider.tag.Equals("wall"))
            {
                manager.GetPlayer().GetComponent<Collider2D>().enabled = false;
                mirrorTimer = 0;
                hitObject = hit.collider.transform.position;
            }
            else
            {
                if (mirrorTimer > mirrorLimit)
                {
                    manager.GetPlayer().GetComponent<Collider2D>().enabled = true;
                    mirrorTimer = 0;
                }
                
            }

        }

        particles.transform.position = hitObject;


        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.startWidth = laserWidth0;
        lineRenderer.endWidth = laserWidth1;

        dist = Vector3.Distance(origin.position, hitObject);

        counter += 0.1f / lineDrawSpeed;

        float x = Mathf.Lerp(0, dist, counter);

        Vector3 pointA = origin.position;
        Vector3 pointB = hitObject;

        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        lineRenderer.SetPosition(1, pointAlongLine);
    }

    private void OnDisable()
    {
        virgin2 = true;
        animator.SetTrigger("barridoToEnding");
        lineRenderer.positionCount = 0;
        if (particles != null)
        {
            particles.SetActive(false);
        }  
        start = false;
        try
        {
            sFX.laser.Stop();
        } catch
        {

        }
        
    }

    private void OnEnable()
    {
        StartCoroutine(ExecLaser());
        lineRenderer.positionCount = defaultLinePosition;
        animator.SetTrigger("barridoToIntro");
        if (!virgin)
            particles.SetActive(true);
    }
}
