using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    public Vector3 direction;
    public Vector3 spawnPosition;
    public float movementSpeed;

    [Header("Walls Colliders")]
    [SerializeField] public GameObject leftCollider;
    [SerializeField] public GameObject rightCollider;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSprite(CheckIfSeen(player.transform) ? sprite : ghostSprite);
        if (!GameStateManager.Instance.isGameOver)
            MoveHorizontally();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public void MoveHorizontally()
    {

        transform.position += direction * movementSpeed * Time.deltaTime;
    }

}
