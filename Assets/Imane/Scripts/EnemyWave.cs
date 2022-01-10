using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyWave : MonoBehaviour
{
    [Header("Enemy Wave Config")]
    public int numberOfWaves;
    public int numberOfEnemiesPerWave;
    public float movementSpeed;
    public float movementSpeedMultiplier;
    public float xAliensSpawnPos;
    public float yAliensSpawnPos;
    public float xUnit;
    public float yUnit;
    public float spawnTimer;
    public Vector3 direction;
    public GameObject enemyWave;

    [Header("Walls Colliders")]
    public GameObject leftCollider;
    public GameObject rightCollider;

    public Alien[,] aliens;

    void Start()
    {

    }

    void Update()
    {
        if (aliens.Length == 0)
        {
            EnemySpawnerManager.Instance.remainingWaves--;
            Destroy(gameObject);
        }

        if (!GameStateManager.Instance.isGameOver)
        {
            MoveHorizontally();
            for (int i = 0; i < aliens.GetLength(1); i++)
                CheckAliensColumn(aliens, i);
            for (int i = 0; i < aliens.GetLength(0); i++)
                CheckAliensRow(aliens, i);
        }
    }

    public void MoveHorizontally()
    {
        enemyWave.transform.position += direction * movementSpeed * Time.deltaTime;

        foreach (Transform enemy in enemyWave.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
                continue;

            if (direction == Vector3.right && enemy.position.x >= rightCollider.transform.position.x - 1.0f)
            {
                MoveVertically();
                break;
            }
            else if (direction == Vector3.left && enemy.position.x <= leftCollider.transform.position.x + 1.0f)
            {
                MoveVertically();
                break;
            }
        }
    }

    public void MoveVertically()
    {
        movementSpeed *= movementSpeedMultiplier;
        //Time.timeScale += GameStateManager.Instance.timeScaleIncrease;
        direction = new Vector3(-direction.x, 0.0f, 0.0f);

        Vector3 position = enemyWave.transform.position;
        position.y -= 1.0f;

        enemyWave.transform.position = position;
    }

    public void CheckAliensColumn(Alien[,] matrix, int columnNumber)
    {
        Alien[] aliens = Enumerable.Range(0, matrix.GetLength(0)).Select(x => matrix[x, columnNumber]).ToArray();

        foreach (Alien alien in aliens)
        {
            if (alien != null)
                break;
            this.aliens = RemoveAliensColumn(matrix, columnNumber);
        }

    }

    public Alien[,] RemoveAliensColumn(Alien[,] matrix, int columnNumber)
    {
        Alien[,] result = new Alien[matrix.GetLength(0), matrix.GetLength(1) - 1]; ;

        for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
        {
            for (int k = 0, u = 0; k < matrix.GetLength(1); k++)
            {
                if (k == columnNumber)
                    continue;

                result[j, u] = matrix[i, k];
                u++;
            }
            j++;
        }
        return result;
    }

    public void CheckAliensRow(Alien[,] matrix, int rowNumber)
    {
        Alien[] aliens = Enumerable.Range(0, matrix.GetLength(1)).Select(x => matrix[rowNumber, x]).ToArray();

        foreach (Alien alien in aliens)
        {
            if (alien != null)
                break;
            this.aliens = RemoveAliensRow(matrix, rowNumber);
        }
    }

    public Alien[,] RemoveAliensRow(Alien[,] matrix, int rowNumber)
    {
        Alien[,] result = new Alien[matrix.GetLength(0) - 1, matrix.GetLength(1)]; ;

        for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
        {
            if (i == rowNumber)
                continue;
            for (int k = 0, u = 0; k < matrix.GetLength(1); k++)
            {
                result[j, u] = matrix[i, k];
                u++;
            }
            j++;
        }
        return result;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == rightCollider || collision.gameObject == leftCollider)
        {
            MoveVertically();
        }
    }
}
