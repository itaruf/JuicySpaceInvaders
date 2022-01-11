using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailParticles.Play();
        AudioManager.Instance.PlayAudio("Laser1", Audio.AudioType.SFX);
        Destroy(gameObject, destroyTime);
        StartCoroutine(OnDestroyed());
        //   transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<UFO>() != null)
                if (!other.GetComponent<UFO>().isDead)
                {
                    other.GetComponent<UFO>().GetDamaged(damage);
                    OnHit();
                }

            if (other.GetComponent<Alien>() != null)
                if (!other.GetComponent<Alien>().isDead)
                {
                    other.GetComponent<Alien>().GetDamaged(damage);
                    OnHit();
                }
        }
        else if (other.tag == "BunkerWall")
        {
            OnHit();
            Destroy(gameObject);
        }
    }
}


