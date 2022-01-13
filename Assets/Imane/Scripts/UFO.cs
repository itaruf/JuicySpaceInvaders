using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    [Header("UFO Config")]
    public float spawnTimer;
    public Vector3 direction;
    public Vector3 spawnPosition;
    public float movementSpeed;

    public float UFOMinSpawnTimer;
    public float UFOMaxSpawnTimer;

    [Header("Walls Colliders")]
    public GameObject leftCollider;
    public GameObject rightCollider;

    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        AudioManager.Instance.PlayAudio("Creepy4", Audio.AudioType.SFX,  AudioManager.AudioAction.START, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
            anim.enabled = AnimatorManager.Instance.isAnimating ? true : false;

        //    SetSprite(CheckIfSeen(player.transform) ? sprite : ghostSprite);
        if (!GameStateManager.Instance.isGameOver)
            MoveHorizontally();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == leftCollider || collision.gameObject == rightCollider)
        {
            EnemySpawnerManager.Instance.totalEnemies--;
            Destroy(gameObject);
        }
    }
    public void MoveHorizontally()
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
