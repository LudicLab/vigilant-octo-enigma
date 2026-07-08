using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

class Explosion : Interaction
{
    [SerializeField] ParticleSystem explosionParticle; 
    [SerializeField] float scale;
    public override void Run(GameObject gameObject)
    {
        Destroy(gameObject);
        ParticleSystem explosionParticleInstance = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
        explosionParticleInstance.transform.localScale = Vector3.one * scale;
    }
}