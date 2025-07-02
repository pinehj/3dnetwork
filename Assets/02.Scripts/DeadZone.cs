using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damagedObject = other.GetComponent<IDamageable>();
        if (damagedObject != null)
        {
            damagedObject.TakeDamage(99999, -1);
        }
    }
}
