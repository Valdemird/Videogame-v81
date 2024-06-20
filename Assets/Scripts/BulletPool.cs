using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    public CameraFollow cameraFollow;
    public static BulletPool Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    private List<GameObject> bullets;
    private int poolIncrementSize = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        Initialize(10);
    }

public void ResetPool()
{
    foreach (GameObject bullet in bullets)
    {
        if (bullet != null)
        {
            bullet.SetActive(false);
        }
    }

    bullets.RemoveAll(bullet => bullet == null);
}

    public void Initialize(int size)
    {
        bullets = new List<GameObject>(size);

        for (int i = 0; i < size; ++i)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullets.Add(bullet);
        return bullet;
    }

    public GameObject GetBullet(Vector3 position, Boolean withShake = false)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.SetActive(true);
                cameraFollow.StartShake();
                return bullet;
            }
        }

        // If no inactive bullets are available, create more bullets
        for (int i = 0; i < poolIncrementSize; ++i)
        {
            CreateNewBullet();
        }

        return GetBullet(position);
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector3.zero;
        }
    }
}