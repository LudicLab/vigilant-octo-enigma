using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

class Destroy : Interaction
{
    [SerializeField] ParticleSystem explosionParticle; 
    public override void Run(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}