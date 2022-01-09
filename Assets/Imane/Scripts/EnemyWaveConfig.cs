using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveConfig 
{
    [Header("Enemy Wave Config")]
    public int numberOfWaves;
    public int numberOfEnemiesPerWave;
    public float movementSpeed;
    public float movementSpeedMultiplier;
    public float yAliensSpawnPos;
    public float xUnit;
    public float yUnit;
    public float spawnTimer;
    public Vector3 direction;
    [HideInInspector] public GameObject enemyWave;

    [Header("Walls Colliders")]
    public GameObject leftCollider;
    public GameObject rightCollider;

    void Start()
    {
        
    }

    void Update()
    {

    }
}
