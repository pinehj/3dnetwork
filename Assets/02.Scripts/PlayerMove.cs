using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    [SerializeField] private float _moveSpeed;
    private float _horizontalMoveInput;
    private float _verticalMoveInput;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _yVelocity;
    [SerializeField] private float _gravity = -20f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _horizontalMoveInput = Input.GetAxisRaw("Horizontal");
        _verticalMoveInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(_horizontalMoveInput, 0, _verticalMoveInput).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        _animator.SetFloat("Move", moveDirection.magnitude);
        //transform.forward = Vector3.Lerp(transform.forward, moveDirection, 0.05f);
        if (_controller.isGrounded)
        {
            _yVelocity = -.1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpPower;
            }
        }

        Debug.Log(_controller.isGrounded);

        if (!_controller.isGrounded)
        {
            _yVelocity += _gravity * Time.deltaTime;
        }

        moveDirection.y = _yVelocity;
        _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }
}
