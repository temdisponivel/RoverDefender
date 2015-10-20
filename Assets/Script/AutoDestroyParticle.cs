using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem particle;

    public void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (particle)
        {
            if (!particle.IsAlive())
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}