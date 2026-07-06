using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnTriggerEnter(Collider other)
    {

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}