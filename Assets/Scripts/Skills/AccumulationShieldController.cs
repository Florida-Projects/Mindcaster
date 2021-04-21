using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulationShieldController : MonoBehaviour
{

    GameManager managerScript;
    SFXManager sFX;
    public float timeToDestroy;

    private void Start()
    {
        managerScript = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        sFX = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        sFX.shield.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(RestoreColor());

        if (collision.gameObject.tag.Equals(Globals.TAG_ENEMY_BULLET))
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.name.Equals(Globals.GO_YELLOWBULLET) || collision.gameObject.name.Equals("YellowEgocentrism(Clone)"))
            {
                managerScript.GetPlayer().GetComponent<Shooting>().bullets+= 2;
                GetComponent<SpriteRenderer>().color = new Color(255, 90, 0, 163);
                sFX.absortion.Play();
            } 
            
            else
            
            {
                GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
                managerScript.GetPlayer().GetComponent<PlayerMovement>().TakeDamage(5);
                StartCoroutine(DestroyShield());
                sFX.shieldBreak.Play();
            }    
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 132);
    }

    IEnumerator RestoreColor()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 132);
    }

    IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        sFX.shield.Stop();
    }
}
