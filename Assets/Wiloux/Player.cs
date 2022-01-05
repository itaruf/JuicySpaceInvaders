using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Base")]
    public float speed = 5.0f;
    public GameObject projectile;
    public float projectileCDDuration;
    private float projectileCD;

    [Header("Overheat")]
    public Slider slider;
    private bool isOverheated;
    public float overHeatGain;
    public float overHeatLoss;
    public float overHeatMax;
    private float overHeatValue;
    public int overHeatSmashAmount;
    public int overHeatSmashValue;


    [Header("FOV")]
    public GameObject FieldOfView;
    [Range(0f,360f)] public float fov = 90f;
    public float viewDistance = 50f;
    public int rayCount = 5;
    public float offset = 0;
    float angle = 0f;
    float angleIncrease = 0;
    float _offset = 0;
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

        if (Input.GetKey(KeyCode.Space))
            Shoot();

        if(isOverheated && Input.GetKeyDown(KeyCode.A))
        {
            overHeatSmashValue--;
            if(overHeatSmashValue <= 0)
            {
                isOverheated = false;
                overHeatValue = 0;
            }
        }

        //Losing heat over time
        if(!isOverheated && overHeatValue >= 0)
        overHeatValue -= overHeatLoss;

        slider.value = overHeatValue / overHeatMax;
    }

    private void LateUpdate()
    {
        FieldOfViewInit();
    }

    Vector3 CalculateMovements()
    {
        Vector3 playerPos = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y);

        playerPos.x = Mathf.Clamp(playerPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + (GetComponent<BoxCollider2D>().bounds.size.x / 2), Camera.main.ViewportToWorldPoint(Vector3.one).x - (GetComponent<BoxCollider2D>().bounds.size.x / 2));

        FieldOfView.gameObject.transform.position = new Vector3(0, 0, 0);

        return playerPos;
    }

    void Shoot()
    {
        if (isOverheated)
            return;

        if (projectileCD <= 0)
        {
            overHeatValue += overHeatGain;
            GameObject lastProj = Instantiate(projectile, transform.position, Quaternion.identity);
            Destroy(lastProj, 5f);
            projectileCD = projectileCDDuration;
        }
        if(overHeatValue >= overHeatMax)
        {
            isOverheated = true;
            overHeatSmashValue = overHeatSmashAmount;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(origin, Vector3.one);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVecFromAngle(angle + _offset), viewDistance, layerMask);

    }

    void FieldOfViewInit()
    {
        mesh = new Mesh();
        origin = transform.position;
        angle = 0;
        angleIncrease = fov / rayCount;

        // On ne laisse pas le choix aux GDs lul
        //_offset = Mathf.Rad2Deg * Mathf.Asin(1) + fov / 2;

        // On leur laisse le choix...
        _offset = offset + fov / 2;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVecFromAngle(angle + _offset), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                Debug.DrawRay(origin, GetVecFromAngle(angle + _offset) * viewDistance, Color.green);
                // No hit
                vertex = origin + GetVecFromAngle(angle + _offset) * viewDistance;
            }
            else
            {
                // Hit object
                Debug.DrawRay(origin, GetVecFromAngle(angle + _offset) * viewDistance, Color.blue);
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
        meshFilter.mesh = mesh;
    }



    Vector3 GetVecFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


}
