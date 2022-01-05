using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    public int health;

    public bool isSeen;

    public Player player;

    public Sprite ghostSprite;
    public Sprite sprite;

    public LayerMask SeenLayerMask;

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

    public void GetDamaged(int amount)
    {
        health -= amount;
        if (health <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
