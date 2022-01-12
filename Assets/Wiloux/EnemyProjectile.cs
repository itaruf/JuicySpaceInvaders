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
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    AudioManager.Instance.PlayAudio("HitWall1", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                    break;
                case 1:
                    AudioManager.Instance.PlayAudio("HitWall2", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                    break;
                case 2:
                    AudioManager.Instance.PlayAudio("HitWall3", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                    break;
                case 3:
                    AudioManager.Instance.PlayAudio("HitWall4", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                    break;
            }

            OnHit();
            Destroy(gameObject);
        }
    }

}
