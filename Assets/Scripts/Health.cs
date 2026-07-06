using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    private int currentHealth;
    public int bleedingDamage;
    
    [SerializeField]
    private bool IsLiving = true;
    void Start()
    {
        currentHealth = health;
    }

    public void startBleeding() 
    {
        InvokeRepeating("Bleeding", 1f, 1f); //powtarzanie krwawienia co sekunde
    }

    public void stopBleeding() 
    {
        CancelInvoke("Bleeding");
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; //funkcja na umieranie czy cos ewentualnie animacja lub ragdoll, ale to juz pozniej
            IsLiving = false;
        }
    }

    void Bleeding() //funkcja na krwawienie 
    {
        TakeDamage(bleedingDamage);
    }
}
