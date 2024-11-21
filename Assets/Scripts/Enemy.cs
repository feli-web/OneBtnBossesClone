using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;
    public TextMeshProUGUI lifeText;

    [Header("Shooting")]
    public float bulletSpeed;
    public float shootTime;
    public Transform bulletSpawn;
    public float rotateSpawn;

    [Header("SquareAttack")]
    public float squareAttackTime;
    public float radius;

    [Header("ConeAttack")]
    public float coneAttackTime;

    [Header("ProjectileAttack")]
    public float projectileSpeed;
    public float projectileAttackTime;

    void Start()
    {
        lifeText.text = life.ToString();
        InvokeRepeating("Shoot", 0, shootTime);
        StartCoroutine(SquareAttack());
        StartCoroutine(ConeAttack());
        StartCoroutine(ProjectileAttack());
    }

    void FixedUpdate()
    {
        bulletSpawn.gameObject.transform.Rotate(0, 0, rotateSpawn);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(Damage());
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        life--;
        lifeText.text = life.ToString();
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        if (life <= 0)
        {
            Destroy(gameObject);

            Timer t = GameObject.Find("Timer").GetComponent<Timer>();
            t.WinScenary();
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledEnemyBullets();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);

            // Reset and assign velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.velocity = bulletSpawn.right * bulletSpeed * Time.deltaTime;
        }
    }

    public IEnumerator SquareAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(squareAttackTime);

            GameObject square = BulletPool.SharedInstance.GetPooledSquare();
            if (square != null)
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized * radius;
                square.transform.position = randomPoint;
                square.transform.rotation = Quaternion.identity;
                square.SetActive(true);

                SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f);
                square.GetComponent<Collider2D>().enabled = false;

                yield return new WaitForSeconds(2);

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                square.GetComponent<Collider2D>().enabled = true;

                yield return new WaitForSeconds(2);

                square.SetActive(false);
            }
        }
    }

    public IEnumerator ConeAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(coneAttackTime);

            GameObject cone = BulletPool.SharedInstance.GetPooledCone();
            if (cone != null)
            {
                float randomX = Random.value > 0.5f ? -3f : 3f;
                float randomY = Random.value > 0.5f ? -3f : 3f;
                cone.transform.position = new Vector2(randomX, randomY);
                cone.transform.rotation = Quaternion.identity;
                cone.SetActive(true);

                SpriteRenderer sr = cone.GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f);
                cone.GetComponent<Collider2D>().enabled = false;

                yield return new WaitForSeconds(2);

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                cone.GetComponent<Collider2D>().enabled = true;

                yield return new WaitForSeconds(2);

                cone.SetActive(false);
            }
        }
    }

    public IEnumerator ProjectileAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectileAttackTime);

            GameObject projectile = BulletPool.SharedInstance.GetPooledProjectile();
            if (projectile != null)
            {
                projectile.transform.position = Vector2.zero;
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);

                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.velocity = randomDirection * projectileSpeed * Time.deltaTime;
            }
        }
    }
}

