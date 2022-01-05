using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    public int health;
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
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
