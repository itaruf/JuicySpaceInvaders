using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UFOConfig
{
    [Header("UFO Config")]
    public SpriteRenderer spriteRenderer;
    public int health;
    public bool isSeen;
    public Player player;
    public Sprite ghostSprite;
    public Sprite sprite;
    public LayerMask SeenLayerMask;
    public Vector3 direction;
    public Vector3 spawnPosition;
    public float movementSpeed;

    public float UFOMinSpawnTimer;
    public float UFOMaxSpawnTimer;

    [Header("Walls Colliders")]
    public GameObject leftCollider;
    public GameObject rightCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
