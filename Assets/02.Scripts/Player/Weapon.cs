using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttack _attack;

    private void Start()
    {
        _attack = GetComponentInParent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == _attack.transform)
        {
            return;
        }

        if(other.GetComponent<IDamageable>() != null)
        {
            _attack.Hit(other);
        }
    }
}
