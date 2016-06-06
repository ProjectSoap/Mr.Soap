using UnityEngine;
using System.Collections;

public class PlayerCharacterDamage : PlayerCharacterAnimationBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        m_playerCharacterController.state = PlayerCharacterController.DriveState.DamageAfter;
    }
}
