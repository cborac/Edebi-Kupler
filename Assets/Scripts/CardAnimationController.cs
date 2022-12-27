using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAnimationController : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("CardDecayAnimation"))
        {
            Destroy(animator.transform.parent.gameObject);
        }
        else if (stateInfo.IsName("CardMissAnimation"))
        {
            CardSliderController.current.isDeploying = false;
        }
        else if (stateInfo.IsName("EdebiCubeDecayAnimation"))
        {
            CardSliderController.current.isDeploying = false;
            Destroy(EdebiCubeController.current);

            if (GameEngine.cubeList.Count != GameEngine.cubeIndex + 1) EventSystem.current.GetComponent<GameEngine>().CreateCube(++GameEngine.cubeIndex);
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
