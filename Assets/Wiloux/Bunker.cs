using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    public int health = 3;
    private SpriteRenderer spr;


    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        switch(UnityEngine.Random.Range(0,3))
        {
            case 0:
                AudioManager.Instance.PlayAudio("HitBarricade1", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 1:
                AudioManager.Instance.PlayAudio("HitBarricade2", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 2:
                AudioManager.Instance.PlayAudio("HitBarricade3", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
        }

        health--;
        if (health > 0)
        {
            spr.sprite = sprites[health];
        }
        else
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            this.gameObject.SetActive(false);
        }
    }

}
