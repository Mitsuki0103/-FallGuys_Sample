using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    // Ray�̒���
    [SerializeField] private float _rayLength = 1f;

    // Ray���ǂꂭ�炢�g�̂ɂ߂荞�܂��邩
    [SerializeField] private float _rayOffset;

    // Ray�̔���ɗp����Layer
    [SerializeField] private LayerMask _layerMask = default;

    private CharacterController _characterController;
    private bool _isGround;

    private void Start()
    {
        // CharacterController���擾
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // �ڒn����
        _isGround = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        // �������̏����ʒu�Ǝp��
        // �኱�g�̂ɂ߂荞�܂����ʒu���甭�˂��Ȃ��Ɛ���������ł��Ȃ���������
        var ray = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        // Raycast��hit���邩�ǂ����Ŕ���
        // ���C���̎w���Y�ꂸ��
        return Physics.Raycast(ray, _rayLength, _layerMask);
    }

    // Debug�p��Ray����������
    private void OnDrawGizmos()
    {
        // �ڒn���莞�͗΁A�󒆂ɂ���Ƃ��͐Ԃɂ���
        Gizmos.color = _isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.down * _rayLength);
    }

    public bool isGrounded()
    {
        return _isGround;
    }
}

