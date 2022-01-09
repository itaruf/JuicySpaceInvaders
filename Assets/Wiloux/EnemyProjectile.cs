using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Wall" )
        {
            //PlayerDeathIg
            Destroy(gameObject);
        }
    }
}
