using UnityEngine;
using System.Collections;

public class PlayerCharacterAnimationBehaviour : StateMachineBehaviourExtend
{
    protected PlayerCharacterController m_playerCharacterController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerCharacterController = animator.gameObject.GetComponent<PlayerCharacterController>();

        m_playerCharacterController.stateMachineBehaviour = this;
    }
}
