using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<UFO>() != null)
                other.GetComponent<UFO>().GetDamaged(damage);

            if (other.GetComponent<Alien>() != null)
                other.GetComponent<Alien>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }
}
