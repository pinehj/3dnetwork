using UnityEngine;

public class PlayerAttack : PlayerAbility
{
    private float _attackTimer = 0;

    protected override void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        
        if (_attackTimer <= 0 && Input.GetMouseButtonDown(0))
        {
            _attackTimer = 1 / _owner.Stat.AttackSpeed;
            int randomAttackIndex = Random.Range(1, 4);
            _owner.Animator.SetTrigger($"Attack{randomAttackIndex}");
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }
}
