using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;

    [Header("Shooting")]
    public GameObject bullet;
    public float bulletSpeed;
    public float shootTime;
    public Transform bulletSpawn;
    public float rotateSpawn;

    [Header("SquareAttack")]
    public GameObject square;
    public float squareAttackTime;
    public float radius = 5f;
    
    [Header("ConeAttack")]
    public GameObject cone;
    public float coneAttackTime;
    
    [Header("ProjectileAttack")]
    public GameObject projectile;
    public float projectileSpeed;
    public float projectileAttackTime;

    void Start()
    {
        InvokeRepeating("Shoot", 0, shootTime);
        StartCoroutine(SquareAttack());
        StartCoroutine(ConeAttack());
        StartCoroutine(ProjectileAttack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletSpawn.gameObject.transform.Rotate(0, 0, rotateSpawn);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        life--;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);
        if (life <= 0)
        {
            Destroy(gameObject);

            Timer t = GameObject.Find("Timer").GetComponent<Timer>();
            t.WinScenary();
        }
    }

    void Shoot()
    {
        var a = Instantiate(bullet, new Vector3(1,0,0), Quaternion.Euler(0,0,0));
        a.gameObject.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * (bulletSpeed +1) * Time.deltaTime;
        
        var b = Instantiate(bullet, new Vector3(-1,0,0), Quaternion.Euler(0,0,0));
        b.gameObject.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * -(bulletSpeed +2) * Time.deltaTime;
        
        var c = Instantiate(bullet, new Vector3(0,1,0), Quaternion.Euler(0,0,0));
        c.gameObject.GetComponent<Rigidbody2D>().velocity = bulletSpawn.up * (bulletSpeed +3) * Time.deltaTime;
        
        var d = Instantiate(bullet, new Vector3(0,-1,0), Quaternion.Euler(0,0,0));
        d.gameObject.GetComponent<Rigidbody2D>().velocity = bulletSpawn.up * -(bulletSpeed) * Time.deltaTime;
    }
    public IEnumerator SquareAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(squareAttackTime);

            Vector2 randomPoint = Random.insideUnitCircle.normalized * radius;
            var s = Instantiate(square, randomPoint, Quaternion.identity);

            Color currentColor = s.GetComponent<SpriteRenderer>().color;
            currentColor.a = 0.1f; 
            s.GetComponent<SpriteRenderer>().color = currentColor;

            s.GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(2);

            Color currentColor2 = s.GetComponent<SpriteRenderer>().color;
            currentColor2.a = 1f; 
            s.GetComponent<SpriteRenderer>().color = currentColor2;

            s.GetComponent<Collider2D>().enabled = true;

            yield return new WaitForSeconds(2);

            Destroy(s);
        }
    }
    public IEnumerator ConeAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(coneAttackTime);

            float randomX = Random.value > 0.5f ? -3f : 3f;
            float randomY = Random.value > 0.5f ? -3f : 3f;
            var s = Instantiate(cone, new Vector2(randomX,randomY), Quaternion.identity);

            Color currentColor = s.GetComponent<SpriteRenderer>().color;
            currentColor.a = 0.1f; 
            s.GetComponent<SpriteRenderer>().color = currentColor;

            s.GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(2);

            Color currentColor2 = s.GetComponent<SpriteRenderer>().color;
            currentColor2.a = 1f; 
            s.GetComponent<SpriteRenderer>().color = currentColor2;

            s.GetComponent<Collider2D>().enabled = true;

            yield return new WaitForSeconds(2);

            Destroy(s);
        }
    }
    public IEnumerator ProjectileAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectileAttackTime);

            var s = Instantiate(projectile, new Vector2(0,0), Quaternion.identity);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            s.GetComponent<Rigidbody2D>().velocity = randomDirection * projectileSpeed * Time.deltaTime;
        }
    }

}
