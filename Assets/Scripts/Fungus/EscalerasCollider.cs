using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision2D = UnityEngine.Collision2D;

public class EscalerasCollider : MonoBehaviour
{
    public Flowchart stairFlowchart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL)) {           
            stairFlowchart.SetBooleanVariable(Globals.FUNGUS_BOOL_IS_ON_STAIRS, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(Globals.TAG_PLAYER_HOTEL))
        {
            stairFlowchart.SetBooleanVariable(Globals.FUNGUS_BOOL_IS_ON_STAIRS, false);
        }
    }
}
