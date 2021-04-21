using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkFeedback : MonoBehaviour
{

    Text text;
    Color startColor = new Color(255, 255, 255, 0);
    Color endColor = new Color(255, 255, 255, 255);
    public string playerTag;
    public KeyCode talkInput;
    public GameObject conversation;
    bool started;
    bool nearTarget;

    private void Start()
    {
        text = transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        if (nearTarget && Input.GetKeyDown(talkInput) && !started)
        {
            HideText();       
            started = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (conversation != null)
            {
                conversation.SetActive(true);
                conversation.GetComponent<Fungus.Flowchart>().enabled = true;
            }
            UnhideText();
            nearTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (conversation != null)
            {
                conversation.GetComponent<Fungus.Flowchart>().enabled = false;
            }
            HideText();
            started = false;
            nearTarget = false;
        }
    }

    public void UnhideText()
    {
        text.color = endColor;
    }

    public void HideText()
    {
        text.color = startColor;
    }

}
