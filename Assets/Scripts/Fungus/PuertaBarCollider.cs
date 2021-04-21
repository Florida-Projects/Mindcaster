using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision2D = UnityEngine.Collision2D;

public class PuertaBarCollider : MonoBehaviour
{
    public Flowchart puertaBar_flowchart;

    private void OnCollisionEnter2D(Collision2D collision)
    {    
        puertaBar_flowchart.gameObject.SetActive(true);   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        puertaBar_flowchart.gameObject.SetActive(false);
    }
}
