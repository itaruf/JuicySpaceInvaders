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

    public GameObject particlesParent;
    public bool stopParticles = false;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        
    }
    public void PlayParticle(ParticleSystem particleSystem, Transform transform, bool shouldDestroy = true)
    {
        if (stopParticles)
            return;

        ParticleSystem onDestroyParticles = Instantiate(particleSystem, transform.position, transform.rotation);
        onDestroyParticles.transform.parent = particlesParent.transform;
        onDestroyParticles.Play();
        if (shouldDestroy)
            Destroy(onDestroyParticles.gameObject, onDestroyParticles.main.duration);
    }

    public void PlayParticles(ParticleSystem particleSystem)
    {
        if (stopParticles)
            return;

        particleSystem.Play();
    }

    public void DisableAllActiveParticles()
    {
        foreach (ParticleSystem particleSystem in particlesParent.GetComponentsInChildren<ParticleSystem>())
            if (particleSystem.isPlaying)
                particleSystem.Stop();
    }

    public void EnableAllStoppedParticles()
    {
        foreach (ParticleSystem particleSystem in particlesParent.GetComponentsInChildren<ParticleSystem>())
            if (!particleSystem.isPlaying)
                particleSystem.Play();
    }
}
