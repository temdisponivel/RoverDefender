using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem particleSystem;

    public void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (particleSystem)
        {
            if (!particleSystem.IsAlive())
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}