using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.IO;
using UnityEngine;

public class PlayerMove : PlayerAbility, IPunObservable
{
    private CharacterController _controller;

    Vector3 _receivedPosition = Vector3.zero;
    Quaternion _receivedRotation = Quaternion.identity;


    [SerializeField] private float _moveSpeed;

    private float _yVelocity;
    private float _gravity = -10f;

    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<CharacterController>();
    }


    // 데이터 동기화를 위한 데이터 전송 및 수신 기능
    // stream : 서버에서 주고받을 데이터가 담겨있는 변수
    // info   : 송수신 성공/실패 여부에 댇한 로그
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {

            // 보내준 순서대로 받는다.
            _receivedPosition = (Vector3)stream.ReceiveNext();
            _receivedRotation = (Quaternion)stream.ReceiveNext();
        }
    }
    private void Update()
    {
        if (!_owner.PhotonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, _receivedPosition, Time.deltaTime * 20f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _receivedRotation, Time.deltaTime * 20f);

            return;
        }
        float horizontalMoveInput = Input.GetAxisRaw("Horizontal");
        float verticalMoveInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMoveInput, 0, verticalMoveInput).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        _owner.Animator.SetFloat("Move", moveDirection.magnitude);
        //transform.forward = Vector3.Lerp(transform.forward, moveDirection, 0.05f);

        JumpAndFalling();
        RunAndWalk();

        moveDirection.y = _yVelocity;
        _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }

    private void JumpAndFalling()
    {
        if (_controller.isGrounded)
        {
            _owner.State.IsJumping = false;
            _yVelocity = -.1f;
            if (Input.GetKeyDown(KeyCode.Space) && _owner.GetAbility<PlayerStamina>().TryConsumeStamina(_owner.Stat.JumpStaminaCost))
            {
                _owner.State.IsJumping = true;
                _yVelocity = _owner.Stat.JumpPower;
            }
        }

        else
        {
            _yVelocity += _gravity * Time.deltaTime;
        }
    }

    private void RunAndWalk()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _owner.GetAbility<PlayerStamina>().TryConsumeStamina(_owner.Stat.RunStaminaCost * Time.deltaTime))
        {
            _owner.State.IsRunning = true;
            _moveSpeed = Mathf.Lerp(_moveSpeed, _owner.Stat.RunSpeed, Time.deltaTime * 20);
        }
        else
        {
            _owner.State.IsRunning = false;
            _moveSpeed = Mathf.Lerp(_moveSpeed, _owner.Stat.WalkSpeed, Time.deltaTime * 20);
        }
        _owner.Animator.SetFloat("Speed", (_moveSpeed - _owner.Stat.WalkSpeed) / (_owner.Stat.RunSpeed - _owner.Stat.WalkSpeed));
    }
}
