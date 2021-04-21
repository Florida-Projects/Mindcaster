using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EgocentrismSkill : MonoBehaviour
{
    Vector3 position;
    public float speed;
    public float area;
    public float radius;
    public int numberOfBullets;
    public GameObject RedBullet;
    public int rotationSpeed;
    public GameObject egocentrismRotation;
    public GameObject YellowBullet;
    public float skillEntryTime;
    public List<GameObject> bulletEgocentrism = new List<GameObject>();
    private bool hasBalls;
    Animator animator;
    AISkillManager skillManager;
    bool firstStage;
    bool started;

    private void Update()
    {
        if (started)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);

            if (transform.position == position)
            {
                position = MyUtilities.RandomPointInBox(Vector3.zero, Vector3.one * area);
            }
            if (hasBalls)
            {

                egocentrismRotation.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
        }    
     
     }

    public IEnumerator InstantiateBalls(int number) {

        yield return new WaitForSeconds(skillEntryTime);
        started = true;

        for (int i=0; i<number; i++) {

            float angle = i * Mathf.PI * 2 / number;
            Vector3 pos = new Vector3(Mathf.Cos(angle)*radius, Mathf.Sin(angle) * radius, 0f);
            GameObject selectedBullet;
            selectedBullet = Random.Range(0, 101) >= 50 ? RedBullet : YellowBullet;
            GameObject bullet = Instantiate(selectedBullet, transform.position+pos, transform.rotation);
            bullet.transform.parent = egocentrismRotation.transform;
            bulletEgocentrism.Add(bullet);
            hasBalls = true;
        }     
    }

    public void DestroyAllBullets() {

       
            for (int i=0; i<bulletEgocentrism.Count; i++) {
                Destroy(bulletEgocentrism[i].gameObject);
            }          
             bulletEgocentrism.Clear();
            hasBalls = false;
      
    }

    private void OnEnable()
    {
        position = MyUtilities.RandomPointInBox(Vector3.zero, Vector3.one * area);
        animator = GetComponent<Animator>();
        skillManager = GetComponent<AISkillManager>();
        firstStage = skillManager.firstStage;
        StartCoroutine(InstantiateBalls(numberOfBullets));

        if (firstStage)
        {
            animator.SetTrigger("ego1ToIntro");
        }

        else
        {
            animator.SetTrigger("ego2ToIntro");
        }

    }

    private void OnDisable()
    {
        DestroyAllBullets();

        if (firstStage)
        {
            animator.SetTrigger("ego1ToEnding");
        }

        else
        {
            animator.SetTrigger("ego2ToEnding");
        }

        started = false;
    }

}
