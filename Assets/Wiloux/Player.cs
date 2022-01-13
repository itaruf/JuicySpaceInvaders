using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Base")]
    public int health = 3;
    public float speed = 5.0f;
    public GameObject projectile;
    public float projectileCDDuration;
    private float projectileCD;
    public ParticleSystem onShootParticles;
    public ParticleSystem onDeathParticles;
    public ParticleSystem onMoveParticles;
    public ParticleSystem onHitParticles;
    public bool isDead = false;
    private Vector3 lastPos;

    [Header("Overheat")]
    public Slider slider;
    private bool isOverheated;
    public float overHeatGain;
    public float overHeatLoss;
    public float overHeatMax;
    private float overHeatValue;
    public int overHeatSmashAmount;
    public int overHeatSmashValue;
    public GameObject overHeatPopUp;

    [Header("Energy")]
    public Vector2 FOVMinMax;
    public Vector2 DistanceMinMax;
    public float EnergyMax;
    public float Energy;
    public bool isCharging;
    public float chargeAmount;

    [Header("FOV")]
    public GameObject FieldOfView;
    [Range(0f, 360f)] public float fov = 90f;
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
    public Material fovColor;
    private Vector2 fovColorFlickDefaultValue;

    private Animator anim;

    public Material cameraGlitch;

    public TextMeshProUGUI textHealth;

    [Header("Camera Shake On Shoot")]
    public CameraShakeConfig cameraShakeOnShootSuccess;
    public CameraShakeConfig cameraShakeOverheatSmash;

    // Start is called before the first frame update
    void Start()
    {
        projectileCD = projectileCDDuration;
        angleIncrease = fov / rayCount;

        anim = GetComponent<Animator>();
        meshFilter = GetComponentInChildren<MeshFilter>();

        FieldOfViewInit();

        lastPos = transform.position;

        Energy = EnergyMax;

        fovColorFlickDefaultValue = new Vector2(0.1f, 0.15f);

        cameraGlitch.SetFloat("StaticAmount", 0f);

        textHealth.text = health.ToString();
    }


    void UpdateFOV()
    {
        fov = Mathf.Lerp(FOVMinMax.x, FOVMinMax.y, Energy / EnergyMax);
        viewDistance = Mathf.Lerp(DistanceMinMax.x, DistanceMinMax.y, Energy / EnergyMax);

        if (Energy >= EnergyMax)
        {
            Energy = EnergyMax;
        }

        if (Energy >= 0)
        {
            Energy -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {

        UpdateFOV();

        if (anim != null)
            anim.enabled = AnimatorManager.Instance.isAnimating ? true : false;

        cameraGlitch.SetFloat("UnscaledTime", Time.unscaledTime);

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDmg(1);
        };

        overHeatPopUp.SetActive(isOverheated);

        Energy += isCharging ? chargeAmount : 0;

        if (isCharging)
            fovColor.SetVector("FlickerMinMax", new Vector2(0.5f, 0.5f));
        else
            fovColor.SetVector("FlickerMinMax", fovColorFlickDefaultValue);

        if (projectileCD >= 0)
            projectileCD -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            Shoot();

        if (isOverheated && Input.GetKeyDown(KeyCode.A))
        {
            overHeatSmashValue--;

            CameraShake.Instance.OnStartShakeCamera(cameraShakeOverheatSmash.duration, cameraShakeOverheatSmash.magnitude, cameraShakeOverheatSmash.minRange, cameraShakeOverheatSmash.maxRange, cameraShakeOverheatSmash.shakeType);

            if (overHeatSmashValue <= 0)
            {
                isOverheated = false;
                overHeatValue = 0;
            }
        }

        //Losing heat over time
        if (!isOverheated && overHeatValue >= 0)
            overHeatValue -= overHeatLoss;

        slider.value = overHeatValue / overHeatMax;

        if (isDead)
            return;
        lastPos = transform.position = CalculateMovements();

    }

    public void TakeDmg(int dmg)
    {
        ParticlesManager.Instance.PlayParticles(onHitParticles);

        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                AudioManager.Instance.PlayAudio("PlayerHurt1", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 1:
                AudioManager.Instance.PlayAudio("PlayerHurt2", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 2:
                AudioManager.Instance.PlayAudio("PlayerHurt3", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
        }

        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                AudioManager.Instance.PlayAudio("Glitch1", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 1:
                AudioManager.Instance.PlayAudio("Glitch2", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 2:
                AudioManager.Instance.PlayAudio("Glitch3", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
        }

        health -= dmg;
        textHealth.text = health.ToString();

        StartCoroutine(StopTime());

        if (health <= 0)
        {
            anim.SetTrigger("death");

            StartCoroutine(GameStateManager.Instance.GameOver(true));
        }
    }

    public GameObject thunder;
    IEnumerator StopTime()
    {
        Time.timeScale = 0;
        cameraGlitch.SetFloat("StaticAmount", 0.5f);
        FindObjectOfType<ThunderManager>().ForceThunder();
        yield return new WaitForSecondsRealtime(2f);
        thunder.SetActive(false);
        cameraGlitch.SetFloat("StaticAmount", 0f);
        Time.timeScale = 1;
    }



    private void LateUpdate()
    {
        FieldOfViewInit();
    }

    Vector3 CalculateMovements()
    {
        Vector3 playerPos = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y);

        if (playerPos != lastPos)
            ParticlesManager.Instance.PlayParticles(onMoveParticles);

        playerPos.x = Mathf.Clamp(playerPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + (GetComponent<BoxCollider2D>().bounds.size.x / 2), Camera.main.ViewportToWorldPoint(Vector3.one).x - (GetComponent<BoxCollider2D>().bounds.size.x / 2));

        FieldOfView.gameObject.transform.position = new Vector3(0, 0, 0);

        return playerPos;
    }

    void Shoot()
    {
        if (isOverheated)
            return;

        if (Energy <= 0)
            return;

        if (projectileCD <= 0)
        {
            CameraShake.Instance.OnStartShakeCamera(cameraShakeOnShootSuccess.duration, cameraShakeOnShootSuccess.magnitude, cameraShakeOnShootSuccess.minRange, cameraShakeOnShootSuccess.maxRange, cameraShakeOnShootSuccess.shakeType);

            AudioManager.Instance.PlayAudio("Laser1", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
            anim.SetTrigger("shoot");

            ParticlesManager.Instance.PlayParticles(onShootParticles); // SFX to play when the player is shooting

            overHeatValue += overHeatGain;

            GameObject lastProj = Instantiate(projectile, transform.position, Quaternion.identity);
            lastProj.transform.parent = ParticlesManager.Instance.particlesParent.transform;
            //ParticlesManager.Instance.PlayParticles(lastProj.GetComponentInChildren<ParticleSystem>());

            projectileCD = projectileCDDuration;
        }
        if (overHeatValue >= overHeatMax)
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
                //   Debug.DrawRay(origin, GetVecFromAngle(angle + _offset) * viewDistance, Color.green);
                // No hit
                vertex = origin + GetVecFromAngle(angle + _offset) * viewDistance;
            }
            else
            {
                // Hit object
                //  Debug.DrawRay(origin, GetVecFromAngle(angle + _offset) * viewDistance, Color.blue);
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

    public IEnumerator OnDestroyed()
    {
        isDead = true;
        ParticlesManager.Instance.PlayParticles(onDeathParticles);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Energy"))
        {
            isCharging = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Energy")
            isCharging = false;
    }
}
