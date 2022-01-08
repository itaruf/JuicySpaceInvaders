using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveConfig 
{
    [SerializeField] public int numberOfWaves;
    [SerializeField] public int numberOfEnemiesPerWave;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float yAliensSpawnPos;
    [SerializeField] public float xUnit;
    [SerializeField] public float yUnit;
    [SerializeField] public Vector3 direction;
    [SerializeField] public GameObject enemyWave;

    [Header("Walls Colliders")]
    [SerializeField] public GameObject leftCollider;
    [SerializeField] public GameObject rightCollider;

    void Start()
    {
        
    }

    void Update()
    {

    }
}
