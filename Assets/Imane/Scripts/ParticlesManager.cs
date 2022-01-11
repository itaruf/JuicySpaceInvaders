using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public Coroutine coroutine;
    private static ParticlesManager _instance;
    public static ParticlesManager Instance {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void PlayParticle(ParticleSystem particleSystem, Transform transform, bool shouldDestroy = true)
    {
        ParticleSystem onDestroyParticles = Instantiate(particleSystem, transform.position, transform.rotation);
        onDestroyParticles.Play();
        if (shouldDestroy)
            Destroy(onDestroyParticles.gameObject, onDestroyParticles.main.duration);
    }
}
