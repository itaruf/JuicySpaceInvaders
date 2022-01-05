using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Enemy Models")]
    public Alien alienModel;
    public List<Sprite> alienSprites;
    public Transform aliensStartPoint;

    public UFO ufoModel;
    public List<Sprite> ufoSprites;
    public Transform ufoLeftStartPoint;
    public Transform ufoRightStartPoint;
    private UFO _ufo;

    public List<Enemy> enemies;
    public GameObject matrixOfEnemies; // Dans la hiérarchie

    private int _currentAlienSpriteIndex = 0;
    private int _currentUfoSpriteIndex = 0;

    [Header("Enemy Numbers")]
    public int numberOfAliensWaves;
    public int numberOfAliensPerWave;

    public float yAliensSpawnPos;

    public float xUnit;
    public float yUnit;

    public float ufoMovementSpeed;
    public float ufoMovementSpeedMultiplier;

    public float aliensMovementSpeed;
    public float aliensMovementSpeedMultiplier;

    private Vector3 _direction;
    private Vector3 _ufoDirection;

    [Header("Walls Colliders")]
    public GameObject leftCollider;
    public GameObject rightCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
            _direction = Vector3.right;
        else 
            _direction = Vector3.left;

        SpawnAliens();
        SpawnUFO();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontally();
        MoveUfoHorizontally();
    }

    void MoveHorizontally()
    {
        matrixOfEnemies.transform.position += _direction * aliensMovementSpeed * Time.deltaTime;

        foreach (Transform enemy in matrixOfEnemies.transform)
        {
            if (!enemy.gameObject.activeInHierarchy) 
                continue;

            if (_direction == Vector3.right && enemy.position.x >= rightCollider.transform.position.x - 1.0f)
            {
                MoveVertically();
                break;
            }
            else if (_direction == Vector3.left && enemy.position.x <= leftCollider.transform.position.x + 1.0f)
            {
                MoveVertically();
                break;
            }
        }
    }

    void MoveVertically()
    {
        aliensMovementSpeed *= aliensMovementSpeedMultiplier;
        ufoMovementSpeed *= ufoMovementSpeedMultiplier;

        _direction = new Vector3(-_direction.x, 0.0f, 0.0f);

        Vector3 position = matrixOfEnemies.transform.position;
        position.y -= 1.0f;

        matrixOfEnemies.transform.position = position;
    }
    public void SpawnAliens()
    {
        float currentXPosition = InitialXAliensPos();
        float currentYPosition = yAliensSpawnPos;

        for (int i = 0; i < numberOfAliensWaves; i++)
        {
            for (int j = 0; j < numberOfAliensPerWave; j++)
            {
                Vector3 pos = new Vector3(currentXPosition, currentYPosition);
                Quaternion rot = alienModel.transform.rotation;
                Vector3 scale = alienModel.transform.localScale;

                Alien alien = GameObject.Instantiate<Alien>(alienModel, pos, rot);
                alien.transform.localScale = scale;
                alien.SetSprite(alienSprites[_currentAlienSpriteIndex]);
                alien.transform.parent = matrixOfEnemies.transform;

                enemies.Add(alien);
                currentXPosition += xUnit;
            }

            if (i % 2 == 0)
            {
                _currentAlienSpriteIndex++;
            }

            if (_currentAlienSpriteIndex == alienSprites.Count)
            {
                _currentAlienSpriteIndex = 0;
            }

            currentXPosition = InitialXAliensPos();
            currentYPosition -= yUnit;

        }
        _currentAlienSpriteIndex = 0;
    }

    public void SpawnUFO()
    {

        Vector3 pos;
        if (_direction == Vector3.left)
        {
            // On start à droite pour aller vers la gauche
            pos = new Vector3(ufoRightStartPoint.position.x, ufoRightStartPoint.position.y);
        }

        else
        {
            // On start à gauche pour aller vers la droite
            pos = new Vector3(ufoLeftStartPoint.position.x, ufoLeftStartPoint.position.y);
        }

        Quaternion rot = ufoModel.transform.rotation;
        Vector3 scale = ufoModel.transform.localScale;

        _ufo = GameObject.Instantiate<UFO>(ufoModel, pos, rot);
        _ufo.transform.localScale = scale;
        _ufo.SetSprite(ufoSprites[_currentUfoSpriteIndex]);
        _ufoDirection = _direction;
    }

    void MoveUfoHorizontally()
    {
        if (!_ufo)
            return;
        _ufo.transform.position += _ufoDirection * ufoMovementSpeed * Time.deltaTime;
    }

    float InitialXAliensPos()
    {
        float position;
        if (numberOfAliensPerWave % 2 == 0)
            position = (float)(-numberOfAliensPerWave / 2 + 0.5);
        else
            position = (float)(-numberOfAliensPerWave / 2);
        return (position);
    }
}
