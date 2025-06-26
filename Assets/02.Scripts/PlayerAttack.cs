using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackCoolTime;
    private float _attackTimer;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _attackTimer = _attackCoolTime;
    }
    void Update()
    {
        
        if (_attackTimer <= 0 && Input.GetMouseButtonDown(0))
        {
            _attackTimer = _attackCoolTime;
            int randomAttackIndex = Random.Range(1, 4);
            _animator.SetTrigger($"Attack{randomAttackIndex}");
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }
}
