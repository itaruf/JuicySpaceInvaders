using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject projectile;
    public float projectileCDDuration;
    private float projectileCD;

    // Start is called before the first frame update
    void Start()
    {
        projectileCD = projectileCDDuration;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = CalculateMovements();

        if (projectileCD >= 0)
            projectileCD -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

    }

    Vector3 CalculateMovements()
    {
        Vector3 playerPos = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y);

        playerPos.x = Mathf.Clamp(playerPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + (GetComponent<BoxCollider2D>().bounds.size.x / 2), Camera.main.ViewportToWorldPoint(Vector3.one).x - (GetComponent<BoxCollider2D>().bounds.size.x / 2));
        return playerPos;
    }

    void Shoot()
    {
        if (projectileCD <= 0)
        {
            GameObject lastProj = Instantiate(projectile, transform.position, Quaternion.identity);
            Destroy(lastProj, 5f);
            projectileCD = projectileCDDuration;
        }
    }
}
