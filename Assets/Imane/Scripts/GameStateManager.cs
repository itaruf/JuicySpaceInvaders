using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    public bool isGameOver = false;
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool value)
    {
        isGameOver = value;
    }

    public bool IsGameOver()
    {
        return (isGameOver);
    }
}
