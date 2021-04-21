using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPresentation : StateMachineBehaviour
{
    GameManager gameManager;
    CameraShake shake;
    CameraFollow cameraFollow;
    public float shakeTime;
    public float timeToPresention;
    float timer;
    float defaultShakeTime;
    bool virgin = true;
    bool virgin2 = true;
    public float cameraSpeed;
    float defaultCameraSpeed;
    public Flowchart beginFungus;
    SFXManager sFXManager;
    float timeToCrie = 8f;
    private PersistentManagerScript persistentManager;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            persistentManager = GameObject.Find("Persistent Manager").GetComponent<PersistentManagerScript>();
        }
        catch (System.Exception e) {
        
        }
        sFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();   
        gameManager = GameObject.Find(Globals.GO_GAME_MANAGER).GetComponent<GameManager>();
        gameManager.GetMainCamera().GetComponent<CameraFollow>().otherTarget = true;
        gameManager.GetMainCamera().GetComponent<CameraFollow>().alternativeTarget = GameObject.Find(Globals.GO_ENEMY).transform;
        shake = gameManager.GetMainCamera().GetComponent<CameraShake>();
        defaultShakeTime = shake.shakeTime;
        cameraFollow = gameManager.GetMainCamera().GetComponent<CameraFollow>();
        defaultCameraSpeed = cameraFollow.smoothSpeed;
        cameraFollow.smoothSpeed = cameraSpeed;
        if (persistentManager != null)
        {
            if (!persistentManager.startTutorialHasFinished)
            {
                beginFungus = GameObject.Find("FirstTuto").GetComponent<Flowchart>();
                beginFungus.gameObject.SetActive(false);               
            }

        }
        else {

            beginFungus = GameObject.Find("FirstTuto").GetComponent<Flowchart>();
            beginFungus.gameObject.SetActive(false);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;


        if (timer > timeToPresention && virgin)
        {
            GameObject.Find(Globals.GO_ENEMY).transform.Find("travis name").GetComponent<Animator>().enabled = true;
            virgin = false;
            shake.shakeTime = shakeTime;
            shake.ShakeIt();
            
        }

        if (timer > timeToCrie && virgin2)
        {
            sFXManager.bossCries.Play();
            virgin2 = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shake.shakeTime = defaultShakeTime;
        cameraFollow.otherTarget = false;
        cameraFollow.smoothSpeed = defaultCameraSpeed;
      
        if (persistentManager != null)
        {
            if (!persistentManager.startTutorialHasFinished)
            {
                beginFungus.gameObject.SetActive(true);
                beginFungus.SetBooleanVariable("animFinished", true);
                persistentManager.startTutorialHasFinished = true;
            }
            else
            {
                GameObject.Find("Enemie").GetComponent<AISkillManager>().enabled = true;
            }
        }
               
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
