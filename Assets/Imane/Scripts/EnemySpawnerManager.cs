using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public Enemy enemyModel;
    public GameObject enemies;

    public List<Enemy> l_enemies;
    public List<GameObject> spawnPoints;

    public int numberOfWaves;
    public int numberOfEnemiesPerWave;

    public List<Sprite> sprites;
    private int _currentSpriteIndex = 0;

    public float initialXPosition = 0;
    public float initialYPosition = 0;

    public float xStep = 0;
    public float yStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        if (numberOfEnemiesPerWave % 2 == 0)
            initialXPosition = (float)(-numberOfEnemiesPerWave / 2 + 0.5);
        else
            initialXPosition = (float)(-numberOfEnemiesPerWave / 2);

        float currentXPosition = initialXPosition;
        float currentYPosition = initialYPosition;

        for (int i=0; i<numberOfWaves; i++)
        {
            for (int j=0; j<numberOfEnemiesPerWave; j++)
            {
                Vector3 pos = new Vector3(currentXPosition, currentYPosition);
                Quaternion rot = enemyModel.transform.rotation;
                Vector3 scale = enemyModel.transform.localScale;

                Enemy enemy = GameObject.Instantiate<Enemy>(enemyModel, pos, rot);
                enemy.transform.localScale = scale;
                //enemy.sprite = sprites[_currentSpriteIndex];
                enemy.transform.parent = enemies.transform;

                currentXPosition += xStep;
            }
            currentXPosition = initialXPosition;
            currentYPosition += yStep;
        }
        currentYPosition = initialYPosition;
    }
}
