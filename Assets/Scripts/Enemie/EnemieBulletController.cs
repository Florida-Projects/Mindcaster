using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBulletController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals(Globals.TAG_BULLET) && !collision.gameObject.name.Equals(Globals.GO_MIRROR) && !gameObject.name.Equals("RedBullet 1(Clone)") && !gameObject.name.Equals("RedBullet 2(Clone)"))
        {
            Destroy(gameObject);
        }    
    }

}
