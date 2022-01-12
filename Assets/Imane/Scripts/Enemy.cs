using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    public int health;
    public int score;

    public bool isSeen;

    public Player player;

    public Sprite ghostSprite;
    public Sprite sprite;

    public LayerMask SeenLayerMask;

    public  float projectileCD;
    public Vector2 projectileCDDurMinMax;

    public GameObject projectile;

    public ParticleSystem onShootParticles;
    public ParticleSystem onDeathParticles;

    public bool isDead = false;

    void Start()
    {
        //   player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>().sprite;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected bool CheckIfSeen(Transform target)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.position, SeenLayerMask);
        if (hit != null && hit.collider.transform.name != "Player")
        {
            isSeen = false;
            return false;
        }
        else
        {
            isSeen = true;
            return true;
        }
    }

    protected bool CheckIfObstruction()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit && hit.collider != this)
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.blue);
            return false;
        }
    }

    protected void Shoot()
    {
        onShootParticles.Play(); // SFX to play when the enemy is shooting

        GameObject lastProj = Instantiate(projectile, transform.position, Quaternion.identity);
        lastProj.GetComponentInChildren<ParticleSystem>().Play();

        projectileCD = Random.Range(projectileCDDurMinMax.x, projectileCDDurMinMax.y);
    }

    public void GetDamaged(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Score.instance.SetScore(score);
            EnemySpawnerManager.Instance.totalEnemies--;
           StartCoroutine(OnDestroyed());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public IEnumerator OnDestroyed()
    {
        isDead = true;

        if (UnityEngine.Random.Range(0,9) == 0)
            AudioManager.Instance.PlayAudio("AlienDeath2", Audio.AudioType.SFX);
        else
            AudioManager.Instance.PlayAudio("AlienDeath", Audio.AudioType.SFX);
        onDeathParticles.Play();
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
