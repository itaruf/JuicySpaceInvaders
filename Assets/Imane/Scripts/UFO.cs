using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    //    SetSprite(CheckIfSeen(player.transform) ? sprite : ghostSprite);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
