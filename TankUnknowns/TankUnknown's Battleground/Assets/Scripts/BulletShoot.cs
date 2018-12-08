using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletShoot : NetworkBehaviour
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
        if(hasAuthority == false)
        {
            return;
        }

        float fire = Input.GetAxis("Fire1");
        if (canFire)
        {
            if (fire > 0)
            {
                CmdShootBullet();
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

    ////////////////////// COMMANDS
    ///

    [Command]
    void CmdShootBullet()
    {

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, new Quaternion(0, 90, 90, 0));

        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, destroyTime);
    }
}
