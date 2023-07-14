using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : StateMachineBehaviour
{
    private CharacterControlScript controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // CharacterControlScript���擾
        controller = animator.GetComponent<CharacterControlScript>();
        // �ړ��ʂ�������
        controller.velocity = Vector3.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �W�����v�ɑJ�ڂ���
        if (Input.GetButtonDown("Jump") && controller.IsGrounded())
        {
            // Jump�t���O���I��
            animator.SetBool("Jump", true);
            return;
        }

        // ���s�A�j���[�V�����Ǘ��i�ړ����͂����邩�ǂ����j
        if (controller.MoveInput().magnitude > 0.0f)
        {
            animator.SetFloat("Speed", 1f); //�L�������s�̃A�j���[�V����ON
        }
        else
        {
            animator.SetFloat("Speed", 0f); //�L�������s�̃A�j���[�V����OFF
        }

        // �ړ��̃x�N�g�����v�Z
        controller.velocity = controller.TargetDirection() * controller.speed;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
