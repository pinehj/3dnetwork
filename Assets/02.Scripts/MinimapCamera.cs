using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform _target;
    private void Start()
    {
        GameManager.Instance.OnInit += Init;
    }
    
    private void Init()
    {
        _target = GameManager.Instance.Player.transform;
    }
    private void Update()
    {
        if(_target == null)
        {
            return;
        }
        transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
    }
}
