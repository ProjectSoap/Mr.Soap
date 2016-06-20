using UnityEngine;
using System.Collections;

public class PlayerCharacterStart : PlayerCharacterAnimationBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

<<<<<<< HEAD
        m_playerCharacterController.transform.Translate(new Vector3(0, 0, 0.025f));
=======
        //m_playerCharacterController.transform.Translate(new Vector3(0, 0, 0.025f));
        //m_playerCharacterController.transform.Translate(m_playerCharacterController.transform.forward * m_playerCharacterController.velocity);
        m_playerCharacterController.rigidBody.AddRelativeForce(Vector3.forward * m_playerCharacterController.velocity);
        //m_playerCharacterController.rigidBody.velocity = m_playerCharacterController.transform.forward * m_playerCharacterController.velocity;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        m_playerCharacterController.state = PlayerCharacterController.DriveState.Normal;
        //m_playerCharacterController.animator.Play("Normal");
    }
}
