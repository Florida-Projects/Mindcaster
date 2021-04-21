using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBotones : MonoBehaviour
{
   public List<GameObject> botones = new List<GameObject>();
    void destroyAllBotones() {
        for (int i = 0; i < botones.Count; i++) {
            Destroy(botones[i]);
        }
        botones.Clear();
    }

}
