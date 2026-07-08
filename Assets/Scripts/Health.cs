using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    private float currentHealth;
    public int bleedingDamage;
    
    [SerializeField]
    private bool IsLiving = true;
    [SerializeField]
    private Interaction interaction;
    [SerializeField]
    string jsonParams; // json is ok here because this converts only when passed (interaction ran) - **ONCE**
    void Awake()
    {
        currentHealth = health;
        interaction.Prepare(gameObject, jsonParams);
    }

    public void startBleeding() 
    {
        InvokeRepeating(nameof(Bleeding), 1f, 1f); //powtarzanie krwawienia co sekunde
    }

    public void stopBleeding() 
    {
        CancelInvoke(nameof(Bleeding));
    }
    public void TakeDamage(float damage)
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
        interaction.Run(gameObject);
    }
}
