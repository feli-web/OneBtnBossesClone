using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;

    [Header("Shooting")]
    public GameObject bullet;
    public float bulletSpeed;
    public float shootTime;
    public Transform bulletSpawn;
    void Start()
    {
        InvokeRepeating("Shoot", 0, shootTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
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
        }
    }
}
