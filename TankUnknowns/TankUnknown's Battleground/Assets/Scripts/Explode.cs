using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explode : NetworkBehaviour
{

    public GameObject explosion;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
