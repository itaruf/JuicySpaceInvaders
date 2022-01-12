using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(GameStateManager.Instance.GameOver(true));
        }
        /*if (collision.gameObject.CompareTag("Enemy"))
        {
            GameStateManager.Instance.GameOver(true);
        }*/
    }
}
