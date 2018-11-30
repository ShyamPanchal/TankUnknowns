using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{

    public GameObject bulletPrefab;

    public GameObject bulletSpawnPoint;

    public int bulletSpeed;

    public float destroyTime = 2.0f;

    public bool canFire;

    public float fireTimer;

    // Use this for initialization
    void Start()
    {
        canFire = true;
        fireTimer = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float fire = Input.GetAxis("Fire1");
        if (canFire)
        {
            if (fire > 0)
            {
                Fire();
                canFire = false;
            }
        }
        else
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 0.4f)
            {
                fireTimer = 0.0f;
                canFire = true;
            }

        }

    }

    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, new Quaternion(0, 90, 90, 0));

        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;

        Destroy(bullet, destroyTime);
    }
}
