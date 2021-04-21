using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerBulletController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Equals(Globals.GO_REDBULLET) && !collision.gameObject.name.Equals(Globals.GO_YELLOWBULLET) && !collision.gameObject.name.Equals(Globals.GO_BOUNDS))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //gameObject.transform.Find("Point Light 2D").GetComponent<Light2D>().intensity += 1.5f; 
            animator.SetBool("pum", true);
            StartCoroutine(DestroyBullet());           
        }

        
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
    }
}
