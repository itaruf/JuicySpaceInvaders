using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float speed = 5f;
    public int damage = 1;
    public ParticleSystem trailParticles;
    public ParticleSystem onDestroyParticles;
    public float destroyTime;

    public CameraShakeConfig cameraShakeOnShootSuccess;
    void Start()
    {
    }

    void Update()
    {
    }

    virtual public IEnumerator OnDestroyed() 
    {
        yield return new WaitForSeconds(destroyTime);
        ParticlesManager.Instance.PlayParticle(onDestroyParticles, transform);
    }

    virtual public void OnHit()
    {
        CameraShake.Instance.OnStartShakeCamera(cameraShakeOnShootSuccess.duration, cameraShakeOnShootSuccess.magnitude, cameraShakeOnShootSuccess.minRange, cameraShakeOnShootSuccess.maxRange, cameraShakeOnShootSuccess.shakeType);

        ParticlesManager.Instance.PlayParticle(onDestroyParticles, transform);

        Destroy(gameObject);
    }
}
