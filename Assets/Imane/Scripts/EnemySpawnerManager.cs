using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Wave Management")]
    [HideInInspector] public int currentWave = 0;
    [HideInInspector] public int remainingWaves = 0;
    public int totalWaves;
    [HideInInspector] public int totalEnemies;
    private float timer;
    public float offset;

    [Header("Alien")]
    public EnemyWaveConfig[] enemyWaveConfigs;
    public GameObject enemyWaveModel;
    public Alien alienModel;
    public List<Sprite> alienSprites;
    private int _currentAlienSpriteIndex = 0;

    [Header("UFO")]
    public UFOConfig UFOConfig;
    public UFO ufoModel;
    public UFO UFO;
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
    private IEnumerator onSpawnAliensCoroutine;
    private IEnumerator onSpawnUFOCoroutine;
    void Start()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        totalWaves = enemyWaveConfigs.Length;
        remainingWaves = totalWaves;
        timer = enemyWaveConfigs[0].spawnTimer;
        totalEnemies = GetAllEnemies();

        onSpawnAliensCoroutine = OnSpawnAliens();
        onSpawnUFOCoroutine = OnSpawnUFO();
        StartCoroutine(onSpawnAliensCoroutine);
        StartCoroutine(onSpawnUFOCoroutine);
    }

    void Update()
    {
        Debug.Log(totalEnemies);
        if (!GameStateManager.Instance.isGameOver && !GameStateManager.Instance.isWon)
        {
        }
        else
        {
            Debug.Log("Stop");
            StopCoroutine(onSpawnAliensCoroutine);
            StopCoroutine(onSpawnUFOCoroutine);
        }
    }

    public IEnumerator OnSpawnAliens()
    {
        while (true)
        {
            SpawnAliens(enemyWaveConfigs[currentWave]);
            yield return new WaitForSeconds(timer);

            if (currentWave == totalWaves)
                break;
        }
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
        enemyWave.movementSpeedMultiplier = enemyWaveConfig.movementSpeedMultiplier;

        enemyWave.yAliensSpawnPos = enemyWaveConfig.yAliensSpawnPos;

        enemyWave.xUnit = enemyWaveConfig.xUnit;
        enemyWave.yUnit = enemyWaveConfig.yUnit;

        enemyWave.spawnTimer = enemyWaveConfig.spawnTimer;
        enemyWave.direction = enemyWaveConfig.direction;
        enemyWave.enemyWave = enemyWaveConfig.enemyWave;

        enemyWave.leftCollider = enemyWaveConfig.leftCollider;
        enemyWave.rightCollider = enemyWaveConfig.rightCollider;

        enemyWave.aliens = new Alien[enemyWave.numberOfWaves, enemyWave.numberOfEnemiesPerWave];

        // float currentXPosition = enemyWave.xAliensSpawnPos;
        float currentXPosition = InitialXAliensPos(enemyWave.numberOfEnemiesPerWave);
        float currentYPosition = enemyWave.yAliensSpawnPos;

        for (int i = 0; i < enemyWaveConfig.numberOfWaves; i++)
        {
            for (int j = 0; j < enemyWave.numberOfEnemiesPerWave; j++)
            {
                Vector3 pos = new Vector3(currentXPosition, currentYPosition);
                Quaternion rot = alienModel.transform.rotation;
                Vector3 scale = alienModel.transform.localScale;

                Alien alien = Instantiate(alienModel, pos, rot);
                alien.transform.localScale = scale;
                alien.sprite = alienSprites[_currentAlienSpriteIndex];
                alien.SetSprite(alien.sprite);
                alien.transform.parent = enemyWaveConfig.enemyWave.transform;

                currentXPosition += enemyWave.xUnit;

                enemyWave.aliens[i, j] = alien;
            }

            if (i % 2 == 0)
                _currentAlienSpriteIndex++;

            if (_currentAlienSpriteIndex == alienSprites.Count)
                _currentAlienSpriteIndex = 0;

            // float currentXPosition = enemyWave.xAliensSpawnPos;
            currentXPosition = InitialXAliensPos(enemyWave.numberOfEnemiesPerWave);
            currentYPosition -= enemyWave.yUnit;

        }
        currentWave++;
        if (currentWave != totalWaves)
            timer = enemyWaveConfigs[currentWave].spawnTimer;
        _currentAlienSpriteIndex = 0;
    }

    public IEnumerator OnSpawnUFO()
    {
        if (UFO)
            yield break;

        float random = Mathf.RoundToInt(UnityEngine.Random.Range(UFOConfig.UFOMinSpawnTimer, UFOConfig.UFOMaxSpawnTimer));
        while (true)
        {
            //Debug.Log(random);
            /*if (totalEnemies == 0)
                break;*/
            if (!UFO)
            {
                yield return new WaitForSeconds(random);
                random = Mathf.RoundToInt(UnityEngine.Random.Range(UFOConfig.UFOMinSpawnTimer, UFOConfig.UFOMaxSpawnTimer));
                SpawnUFO(UFOConfig);
                totalEnemies++;
                StartCoroutine(onSpawnUFOCoroutine);
            }

            yield return null;
        }
    }
    public void SpawnUFO(UFOConfig UFOConfig)
    {
        UFO = Instantiate(ufoModel);

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

    // Forcer le centrage d'une vague
    public float InitialXAliensPos(float numberOfEnemiesPerWave)
    {
        float position;
        if (numberOfEnemiesPerWave % 2 == 0)
            position = (float)(-numberOfEnemiesPerWave / 2 + offset);
        else
            position = (float)(-numberOfEnemiesPerWave / 2 + offset);
        return (position);
    }

    public int GetAllEnemies()
    {
        int totalEnemies = 0;
        foreach(EnemyWaveConfig enemyWaveConfig in enemyWaveConfigs)
        {
            totalEnemies += enemyWaveConfig.numberOfEnemiesPerWave * enemyWaveConfig.numberOfWaves;
        }
        return (totalEnemies);
    }
}
