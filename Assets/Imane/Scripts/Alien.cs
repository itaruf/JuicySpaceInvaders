using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{
 
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        SetSprite(CheckIfSeen(player.transform) ? sprite : ghostSprite);

    }

}
