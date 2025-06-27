using UnityEngine;

public class PlayerRotation : PlayerAbility
{
    // 목표: 마우스를 조작하면 카메라를 그 방향으로 회전시키고 싶다.
    [SerializeField] private Transform _cameraRoot;

    private float _horizontalMouseMove;
    private float _verticalMouseMove;


    private void Update()
    {
        // 1. 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        _horizontalMouseMove += mouseX * _owner.Stat.RotationSpeed * Time.deltaTime;
        _verticalMouseMove += mouseY * _owner.Stat.RotationSpeed * Time.deltaTime;

        _verticalMouseMove = Mathf.Clamp(_verticalMouseMove, -89, 89);


        transform.eulerAngles = new Vector3(0f, _horizontalMouseMove, 0f);

        _cameraRoot.localEulerAngles = new Vector3(-_verticalMouseMove, 0f, 0f);
        // 2. 회전 방향 결정하기
        // 3. 회전하기
    }
}
