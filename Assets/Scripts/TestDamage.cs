using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damage);
        }
    }
}