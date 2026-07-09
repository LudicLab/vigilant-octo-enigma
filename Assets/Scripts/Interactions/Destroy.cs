using UnityEngine;

class Destroy : Interaction
{
    [SerializeField] ParticleSystem explosionParticle;
    public override void Prepare(GameObject owner, string jsonParams) {}
    public override void Run(GameObject gameObject)
    {
        UnityEngine.Object.Destroy(gameObject);
    }
}