﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
    //オンライン化に必要なコンポーネントを設定
    public PhotonView myPV;
    public PhotonTransformView myPTV;

    private Camera mainCam;
    // 移動処理に必要なコンポーネントを設定
    private Animator animator;                 // モーションのコントロール
    private CharacterController controller;    // キャラクター移動の管理
    private RaycastCheck raycast;              // 着地しているかどうかの判定

    // パラメータ用変数(inspectorビューで設定)
    public float speed;         // 移動速度
    public float jumpSpeed;     // ジャンプ力
    public float rotateSpeed;   // 回転速度
    public float gravity;       // 重力

    // 移動処理に必要なベクトル
    public Vector3 velocity;    // 移動量
    Vector3 targetDirection;    // 進行方向のベクトル
    Vector2 input;              // 移動の入力

    // 初期化
    void Start()
    {
        if (myPV.isMine)    //自キャラであれば実行
        {
            // Animatorを取得
            animator = GetComponent<Animator>();
            // CharacterControllerを取得
            controller = GetComponent<CharacterController>();
            // RaycastCheckを取得
            raycast = GetComponent<RaycastCheck>();
            // 移動量を初期化
            velocity = Vector3.zero;
            // 移動の入力を初期化
            input = Vector2.zero;

            //MainCameraのtargetにこのゲームオブジェクトを設定
            mainCam = Camera.main;
            mainCam.GetComponent<CameraScript>().target = this.gameObject.transform;
        }
    }

    // 更新
    void Update()
    {
        if (!myPV.isMine)
        {
            return;
        }
        // 移動処理
        MoveControl();
        // 旋回処理
        RotationControl();
        // 最終的な移動処理
        controller.Move(velocity * Time.deltaTime);
    }

    // 移動処理
    void MoveControl()
    {
        // 移動方向を計算
        input.y = Input.GetAxisRaw("Vertical");         // InputManagerの↑↓の入力       
        input.x = Input.GetAxisRaw("Horizontal");       // InputManagerの←→の入力 

        // カメラの正面方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //カメラの右方向を取得

        // カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = input.x * right + input.y * forward;

        //スムーズな同期のためにPhotonTransformViewに速度値を渡す
        Vector3 velocity = controller.velocity;
        myPTV.SetSynchronizedValues(velocity, 0);
    }

    // 旋回処理
    void RotationControl()
    {
        Vector3 rotateDirection = velocity;
        rotateDirection.y = 0;

        //それなりに移動方向が変化する場合のみ移動方向を変える
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //緩やかに移動方向を変える
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    // 空中移動処理
    public void AirMoveControl()
    {
        float tempy = velocity.y;
        //(↓の２文の処理があると空中でも入力方向に動けるようになる)
        velocity = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
        velocity *= speed;
        velocity.y = tempy - gravity * Time.deltaTime;
    }

    // 地面に着地しているかの取得
    public bool IsGrounded()
    {
        return raycast.isGrounded();
    }

    // 進行方向のベクトルの取得
    public Vector3 TargetDirection()
    {
        return targetDirection;
    }

    // 移動の入力の取得
    public Vector2 MoveInput()
    {
        return input;
    }
}
