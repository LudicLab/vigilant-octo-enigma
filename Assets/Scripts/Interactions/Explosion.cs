using System.Collections.Generic;
using UnityEngine;

class Explosion : Interaction
{
    [System.Serializable]
    internal class Data
    {
        public float scale;
    }

    [SerializeField] ParticleSystem explosionParticle;

    // keyed per-caller instead of one shared field
    // dla każdego próbującego zrozumieć co tu jest napisane:
    // okazuje się że jeśli zcalluje Explosion.cs z kilku skryptów to one tak na prawdę są jednym skryptem
    // możnaby to było naprawić instantiate, ale tak jest czyściej bez tworzenia gównoobiektów
    
    readonly Dictionary<GameObject, Data> pending = new(); 

    public override void Prepare(GameObject owner, string jsonParams)
    {
        pending[owner] = JsonUtility.FromJson<Data>(jsonParams);
    }

    public override void Run(GameObject gameObject)
    {
        if (!pending.TryGetValue(gameObject, out var data))
        {
            Debug.LogWarning($"{gameObject} has no prepared data for Explosion");
            return;
        }
        pending.Remove(gameObject); // avoid leaking entries

        Destroy(gameObject);
        var instance = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
        instance.transform.localScale = Vector3.one * data.scale;
        Debug.Log($"{gameObject} blew up with scale {data.scale}");
    }
}