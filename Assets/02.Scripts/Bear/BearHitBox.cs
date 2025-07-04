using UnityEngine;

public class BearHitBox : MonoBehaviour
{
    private Bear _bear;

    private void Awake()
    {
        _bear = GetComponentInParent<Bear>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.transform == _bear.transform)
        {
            return;
        }
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null && target.CanDamage())
        {
            _bear.Hit(other);
        }
    }
}
