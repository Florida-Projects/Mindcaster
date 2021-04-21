using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILookAt : MonoBehaviour
{
    GameManager gameManager;
    Transform target;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        target = gameManager.GetPlayer().transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDir = target.position - rb.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }
}
