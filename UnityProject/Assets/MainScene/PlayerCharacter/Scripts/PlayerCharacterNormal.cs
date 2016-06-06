using UnityEngine;
using System.Collections;

public class PlayerCharacterNormal : PlayerCharacterAnimationBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateFixedUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_soapDrive.SendMessage("Accel");
        //m_soapDrive.SendMessage("Breake");
        //m_soapDrive.SendMessage("Jump");
    }
}
