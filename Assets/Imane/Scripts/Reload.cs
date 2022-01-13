using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public ParticleSystem energyParticles;
    void Start()
    {
        ParticlesManager.Instance.PlayParticles(energyParticles);
        energyParticles.transform.parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (ParticlesManager.Instance.stopParticles)
        {
            if (!energyParticles.isStopped)
                energyParticles.Stop();
        }
        else if (!ParticlesManager.Instance.stopParticles)
            if (!energyParticles.isPlaying)
                ParticlesManager.Instance.PlayParticles(energyParticles);
    }
}
