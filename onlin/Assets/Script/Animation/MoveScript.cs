using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : StateMachineBehaviour
{
    private CharacterControlScript controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // CharacterControlScriptを取得
        controller = animator.GetComponent<CharacterControlScript>();
        // 移動量を初期化
        controller.velocity = Vector3.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ジャンプに遷移する
        if (Input.GetButtonDown("Jump") && controller.IsGrounded())
        {
            // Jumpフラグをオン
            animator.SetBool("Jump", true);
            return;
        }

        // 走行アニメーション管理（移動入力があるかどうか）
        if (controller.MoveInput().magnitude > 0.0f)
        {
            animator.SetFloat("Speed", 1f); //キャラ走行のアニメーションON
        }
        else
        {
            animator.SetFloat("Speed", 0f); //キャラ走行のアニメーションOFF
        }

        // 移動のベクトルを計算
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
