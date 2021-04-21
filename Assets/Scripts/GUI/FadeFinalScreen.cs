using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeFinalScreen : MonoBehaviour
{

    public Flowchart endFlowchart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("wait25secs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator wait25secs() {        
        yield return new WaitForSeconds(20);
        endFlowchart.gameObject.SetActive(true);
    }
    void GoBackToFistScene() {
        SceneManager.LoadScene(0);
    }
}
