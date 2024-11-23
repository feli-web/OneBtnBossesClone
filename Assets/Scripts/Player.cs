using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 _rotationPoint = Vector2.zero;
    public float speed;
    private int rotationDirection = 1;

    [Header("Shooting")]
    public float bulletSpeed;
    public float shootTime;
    public Transform bulletSpawn;

    [Header("Life")]
    public int life;
    public TextMeshProUGUI lifeText;

    [Header("Dash")]
    private float dashTime = 100;
    private bool isDashing = false;
    private bool dashRecharge = false;
    public TextMeshProUGUI dashText;

    private PlayerController pc;

    void Awake()
    {
        pc = new PlayerController();
    }

    void OnEnable()
    {
        pc.Enable();
        pc.Player.Direction.performed += _ => ChangeOrientation();
        pc.Player.Dash.performed += _ => StartDash();
    }

    void OnDisable()
    {
        pc.Player.Direction.performed -= _ => ChangeOrientation();
        pc.Player.Dash.performed -= _ => StartDash();
        pc.Disable();
    }

    void Start()
    {
        InvokeRepeating("Shoot", 0, shootTime);
        lifeText.text = "Player Life: " + life;
        dashText.text = "Dash: " + dashTime.ToString("F0");
    }

    void FixedUpdate()
    {
        float angle = speed * rotationDirection;
        transform.RotateAround(_rotationPoint, Vector3.forward, angle);

        if (dashRecharge)
        {
            dashTime += Time.fixedDeltaTime * 10;
            if (dashTime >= 100)
            {
                dashTime = 100;
                dashRecharge = false;
            }

            dashText.text = "Dash: " + dashTime.ToString("F0");
        }
    }

    public void ChangeOrientation()
    {
        if (!isDashing)
        {
            rotationDirection *= -1;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(Damage());
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledPlayerBullets();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);

            // Reset and assign velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.velocity = bulletSpawn.up * bulletSpeed * Time.deltaTime;
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        life--;
        lifeText.text = "Player Life: " + life;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        if (life <= 0)
        {
            Destroy(gameObject);

            Timer t = GameObject.Find("Timer").GetComponent<Timer>();
            t.LoseScenary();
        }
    }

    public void StartDash()
    {
        if (!isDashing && dashTime == 100)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.grey;

        float dashDuration = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < dashDuration && dashTime > 0)
        {
            dashTime -= Time.deltaTime * 50;
            elapsedTime += Time.deltaTime;

            dashText.text = "Dash: " + Mathf.Clamp(dashTime, 0, 100).ToString("F0");
            yield return null;
        }

        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;

        isDashing = false;
        dashRecharge = true;

        while (dashRecharge && dashTime < 100)
        {
            dashTime += Time.deltaTime * 10;
            dashText.text = "Dash: " + Mathf.Clamp(dashTime, 0, 100).ToString("F0");
            yield return null;
        }

        dashRecharge = false;
    }
}

