using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Movement")]
    public Vector2 _rotationPoint = Vector2.zero; 
    public float speed;
    private int rotationDirection = 1;

    [Header("Shooting")]
    public GameObject bullet;
    public float bulletSpeed;
    public float shootTime;
    public Transform bulletSpawn;

    [Header("Life")]
    public int life;


    void Start()
    {
        InvokeRepeating("Shoot", 0, shootTime);
    }

    void FixedUpdate()
    {
        float angle = speed * rotationDirection;
        transform.RotateAround(_rotationPoint, Vector3.forward, angle);
    }

    public void ChangeOrientation()
    {
        rotationDirection *= -1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
        }
    }

    void Shoot()
    {
        var a = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        a.gameObject.GetComponent<Rigidbody2D>().velocity = bulletSpawn.up * (bulletSpeed + speed) * Time.deltaTime;
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
            t.LoseScenary();
        }
    }
}
