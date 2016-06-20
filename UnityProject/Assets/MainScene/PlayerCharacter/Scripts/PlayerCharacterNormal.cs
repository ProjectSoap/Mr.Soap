using UnityEngine;
using System.Collections;

public class PlayerCharacterNormal : PlayerCharacterAnimationBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        GameObject meshObject = m_playerCharacterController.meshObject;

        //meshObject.transform.Translate(0.0f, -0.07f, -0.4f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        GameObject meshObject = m_playerCharacterController.meshObject;

        //meshObject.transform.Translate(0.0f, 0.07f, 0.4f);
    }
}
