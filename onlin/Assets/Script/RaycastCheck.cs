using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    // Rayの長さ
    [SerializeField] private float _rayLength = 1f;

    // Rayをどれくらい身体にめり込ませるか
    [SerializeField] private float _rayOffset;

    // Rayの判定に用いるLayer
    [SerializeField] private LayerMask _layerMask = default;

    private CharacterController _characterController;
    private bool _isGround;

    private void Start()
    {
        // CharacterControllerを取得
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // 接地判定
        _isGround = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        // 放つ光線の初期位置と姿勢
        // 若干身体にめり込ませた位置から発射しないと正しく判定できない時がある
        var ray = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        // Raycastがhitするかどうかで判定
        // レイヤの指定を忘れずに
        return Physics.Raycast(ray, _rayLength, _layerMask);
    }

    // Debug用にRayを可視化する
    private void OnDrawGizmos()
    {
        // 接地判定時は緑、空中にいるときは赤にする
        Gizmos.color = _isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.down * _rayLength);
    }

    public bool isGrounded()
    {
        return _isGround;
    }
}

