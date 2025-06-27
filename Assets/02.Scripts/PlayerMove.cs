using Unity.Android.Gradle;
using UnityEngine;

public class PlayerMove : PlayerAbility
{
    private CharacterController _controller;

    private float _horizontalMoveInput;
    private float _verticalMoveInput;
    
    private float _yVelocity;
    [SerializeField] private float _gravity = -20f;

    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _horizontalMoveInput = Input.GetAxisRaw("Horizontal");
        _verticalMoveInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(_horizontalMoveInput, 0, _verticalMoveInput).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        _owner.Animator.SetFloat("Move", moveDirection.magnitude);
        //transform.forward = Vector3.Lerp(transform.forward, moveDirection, 0.05f);
        if (_controller.isGrounded)
        {
            _yVelocity = -.1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _owner.Stat.JumpPower;
            }
        }

        Debug.Log(_controller.isGrounded);

        if (!_controller.isGrounded)
        {
            _yVelocity += _gravity * Time.deltaTime;
        }

        moveDirection.y = _yVelocity;
        _controller.Move(moveDirection * _owner.Stat.MoveSpeed * Time.deltaTime);
    }
}
