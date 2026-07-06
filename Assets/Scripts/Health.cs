using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    private int currentHealth;
    public int bleedingDamage;
    
    [SerializeField]
    private bool IsLiving = true;
    void Awake()
    {
        currentHealth = health;
    }

    public void startBleeding() 
    {
        InvokeRepeating(nameof(Bleeding), 1f, 1f); //powtarzanie krwawienia co sekunde
    }

    public void stopBleeding() 
    {
        CancelInvoke(nameof(Bleeding));
    }
    public void TakeDamage(int damage)
    {
        if(damage <= 0) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; //funkcja na umieranie czy cos ewentualnie animacja lub ragdoll, ale to juz pozniej
            IsLiving = false;
            // Debug.Log($"Player died"); // debug
            Die();
        }

        // Debug.Log($"player took {damage} damage, currently at {currentHealth}"); // debug
    }

    void Bleeding() //funkcja na krwawienie 
    {
        TakeDamage(bleedingDamage);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        // Temporary
        Destroy(gameObject); // TODO: handle player and non-player death
    }
}
