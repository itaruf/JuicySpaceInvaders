using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailParticles.Play();

        Destroy(gameObject, destroyTime);
        StartCoroutine(OnDestroyed());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"  )
        {
            other.GetComponent<Player>().TakeDmg(1);
            OnHit();
            Destroy(gameObject);
        } 
        else if(other.GetComponent<Bunker>())
        {
            OnHit();
            other.GetComponent<Bunker>().TakeDamage();
            Destroy(gameObject);
        }
        else if (other.tag == "BunkerWall") 
        {
            OnHit();
            Destroy(gameObject);
        }
    }

}
