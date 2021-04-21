using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteAfterTalk : MonoBehaviour
{  

    public RuntimeAnimatorController anim;

    void changeSpriteMain() {
        GetComponent<Animator>().runtimeAnimatorController = anim;
    }
}
