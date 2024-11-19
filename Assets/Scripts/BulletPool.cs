using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;

    [Header("Player Bullets")]
    public List<GameObject> pooledPlayerBullets;
    public GameObject playerBulletsToPool;
    public int playerBulletsAmountToPool;

    [Header("Enemy Bullets")]
    public List<GameObject> pooledEnemyBullets;
    public GameObject enemyBulletsToPool;
    public int enemyBulletsAmountToPool;

    [Header("Square")]
    public List<GameObject> pooledSquare;
    public GameObject squareToPool;
    public int squareAmountToPool;
    
    [Header("Cone")]
    public List<GameObject> pooledCone;
    public GameObject coneToPool;
    public int coneAmountToPool;

    [Header("Projectile")]
    public List<GameObject> pooledProjectile;
    public GameObject projectileToPool;
    public int projectileAmountToPool;

    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        pooledPlayerBullets = new List<GameObject>();
        GameObject a;
        for (int i = 0; i < playerBulletsAmountToPool; i++)
        {
            a = Instantiate(playerBulletsToPool);
            a.SetActive(false);
            pooledPlayerBullets.Add(a);
        }

        pooledEnemyBullets = new List<GameObject>();
        GameObject b;
        for (int i = 0; i < enemyBulletsAmountToPool; i++)
        {
            b = Instantiate(enemyBulletsToPool);
            b.SetActive(false);
            pooledEnemyBullets.Add(b);
        }

        pooledSquare = new List<GameObject>();
        GameObject c;
        for (int i = 0; i < squareAmountToPool; i++)
        {
            c = Instantiate(squareToPool);
            c.SetActive(false);
            pooledSquare.Add(c);
        }
        
        pooledCone = new List<GameObject>();
        GameObject d;
        for (int i = 0; i < coneAmountToPool; i++)
        {
            d = Instantiate(coneToPool);
            d.SetActive(false);
            pooledCone.Add(d);
        }

        pooledProjectile = new List<GameObject>();
        GameObject e;
        for (int i = 0; i < projectileAmountToPool; i++)
        {
            e = Instantiate(projectileToPool);
            e.SetActive(false);
            pooledProjectile.Add(e);
        }
    }


    public GameObject GetPooledPlayerBullets()
    {
        for (int i = 0; i < playerBulletsAmountToPool; i++)
        {
            if (!pooledPlayerBullets[i].activeInHierarchy)
            {
                return pooledPlayerBullets[i];
            }
        }
        return null;
    }
    public GameObject GetPooledEnemyBullets()
    {
        for (int i = 0; i < enemyBulletsAmountToPool; i++)
        {
            if (!pooledEnemyBullets[i].activeInHierarchy)
            {
                return pooledEnemyBullets[i];
            }
        }
        return null;
    }
    public GameObject GetPooledSquare()
    {
        for (int i = 0; i < squareAmountToPool; i++)
        {
            if (!pooledSquare[i].activeInHierarchy)
            {
                return pooledSquare[i];
            }
        }
        return null;
    }
    public GameObject GetPooledCone()
    {
        for (int i = 0; i < coneAmountToPool; i++)
        {
            if (!pooledCone[i].activeInHierarchy)
            {
                return pooledCone[i];
            }
        }
        return null;
    }
    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < projectileAmountToPool; i++)
        {
            if (!pooledProjectile[i].activeInHierarchy)
            {
                return pooledProjectile[i];
            }
        }
        return null;
    }
}
