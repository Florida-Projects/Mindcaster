using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnBulletDamage : MonoBehaviour
{
    AICollisionManager collisionManager;

    private void Start()
    {
        collisionManager = gameObject.GetComponent<AICollisionManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_ENEMY_BULLET) && !collision.gameObject.name.Equals("YellowBullet 1(Clone)") && !collision.gameObject.name.Equals("RedBullet 1(Clone)"))
        {
            collisionManager.TakeDamage(collisionManager.bulletDamage);
        }
    }
}
