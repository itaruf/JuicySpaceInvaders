using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject projectile;
    public float projectileCDDuration;
    private float projectileCD;


    public float fov = 90f;
    public float viewDistance = 50f;
    public int rayCount = 5;
    float angle = 0f;
    float angleIncrease = 0;

    public LayerMask layerMask;

    private Vector3 origin;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        projectileCD = projectileCDDuration;
        angleIncrease = fov / rayCount;

        meshFilter = GetComponentInChildren<MeshFilter>();

        FieldOfViewInit();

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

    private void LateUpdate()
    {
        FieldOfViewInit();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(origin, Vector3.one);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVecFromAngle(angle), viewDistance, layerMask);


    }

    void FieldOfViewInit()
    {
        mesh = new Mesh();
        fov = 90f;
        viewDistance = 50f;
        origin = transform.position;
        rayCount = 50;
        angle = 0;
        angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;
        Debug.Log(origin);

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVecFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                Debug.DrawRay(origin, GetVecFromAngle(angle)* viewDistance, Color.green);
                // No hit
                vertex = origin + GetVecFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                Debug.DrawRay(origin, GetVecFromAngle(angle) * viewDistance, Color.red);
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

    }



    Vector3 GetVecFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


}
