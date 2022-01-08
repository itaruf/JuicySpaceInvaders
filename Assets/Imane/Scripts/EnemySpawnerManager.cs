using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Alien")]
    [SerializeField] public EnemyWaveConfig[] enemyWaveConfigs;
    [SerializeField] public GameObject enemyWaveModel;
    public Alien alienModel;
    public List<Sprite> alienSprites;
    private int _currentAlienSpriteIndex = 0;

    [Header("UFO")]
    [SerializeField] public UFOConfig UFOConfig;
    [SerializeField] public UFO ufoModel;

    private static EnemySpawnerManager _instance;
    public static EnemySpawnerManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }
    void Start()
    {
        foreach(EnemyWaveConfig enemyWaveConfig in enemyWaveConfigs)
        {
            SpawnAliens(enemyWaveConfig);
        }
        SpawnUFO(UFOConfig);
    }

    void Update()
    {
        
    }

    public void SpawnAliens(EnemyWaveConfig enemyWaveConfig)
    {
        GameObject enemyWaveParent = Instantiate(enemyWaveModel);
        enemyWaveConfig.enemyWave = enemyWaveParent; // GameObject Parent qui va permettre de bouger toute la vague

        EnemyWave enemyWave = enemyWaveParent.GetComponent<EnemyWave>();

        // Copy-paste data from config

        enemyWave.enemyWave = enemyWaveParent;

        enemyWave.numberOfWaves = enemyWaveConfig.numberOfWaves;
        enemyWave.numberOfEnemiesPerWave = enemyWaveConfig.numberOfEnemiesPerWave;

        enemyWave.movementSpeed = enemyWaveConfig.movementSpeed;

        enemyWave.yAliensSpawnPos = enemyWaveConfig.yAliensSpawnPos;

        enemyWave.xUnit = enemyWaveConfig.xUnit;
        enemyWave.yUnit = enemyWaveConfig.yUnit;

        enemyWave.direction = enemyWaveConfig.direction;
        enemyWave.enemyWave = enemyWaveConfig.enemyWave;

        enemyWave.leftCollider = enemyWaveConfig.leftCollider;
        enemyWave.rightCollider = enemyWaveConfig.rightCollider;

        enemyWave.aliens = new Alien[enemyWave.numberOfWaves, enemyWave.numberOfEnemiesPerWave];

        float currentXPosition = InitialXAliensPos(enemyWave.numberOfEnemiesPerWave);
        float currentYPosition = enemyWave.yAliensSpawnPos;

        for (int i = 0; i < enemyWaveConfig.numberOfWaves; i++)
        {
            for (int j = 0; j < enemyWave.numberOfEnemiesPerWave; j++)
            {
                Vector3 pos = new Vector3(currentXPosition, currentYPosition);
                Quaternion rot = alienModel.transform.rotation;
                Vector3 scale = alienModel.transform.localScale;

                Alien alien = GameObject.Instantiate<Alien>(alienModel, pos, rot);
                alien.transform.localScale = scale;
                alien.sprite = alienSprites[_currentAlienSpriteIndex];
                alien.SetSprite(alien.sprite);
                alien.transform.parent = enemyWaveConfig.enemyWave.transform;

                currentXPosition += enemyWave.xUnit;

                enemyWave.aliens[i, j] = alien;
                Debug.Log(_currentAlienSpriteIndex);

            }

            if (i % 2 == 0)
                _currentAlienSpriteIndex++;

            if (_currentAlienSpriteIndex == alienSprites.Count)
                _currentAlienSpriteIndex = 0;

            currentXPosition = InitialXAliensPos(enemyWave.numberOfEnemiesPerWave);
            currentYPosition -= enemyWave.yUnit;

        }
        _currentAlienSpriteIndex = 0;
    }

    public void SpawnUFO(UFOConfig UFOConfig)
    {

        UFO UFO = Instantiate(ufoModel);

        UFO.spriteRenderer = UFOConfig.spriteRenderer;
        UFO.health = UFOConfig.health;
        UFO.isSeen = UFOConfig.isSeen;
        UFO.player = UFOConfig.player;
        UFO.ghostSprite = UFOConfig.ghostSprite;
        UFO.sprite = UFOConfig.sprite;
        UFO.SeenLayerMask = UFOConfig.SeenLayerMask;
        UFO.direction = UFOConfig.direction;
        UFO.spawnPosition = UFOConfig.spawnPosition;
        UFO.movementSpeed = UFOConfig.movementSpeed;
        UFO.leftCollider = UFOConfig.leftCollider; 
        UFO.rightCollider = UFOConfig.rightCollider;

        UFO.transform.position = UFO.spawnPosition;
        UFO.transform.localScale = ufoModel.transform.localScale;
        UFO.transform.rotation = ufoModel.transform.rotation;
    }

    public float InitialXAliensPos(float numberOfEnemiesPerWave)
    {
        float position;
        if (numberOfEnemiesPerWave % 2 == 0)
            position = (float)(-numberOfEnemiesPerWave / 2 + 0.5);
        else
            position = (float)(-numberOfEnemiesPerWave / 2);
        return (position);
    }
}
