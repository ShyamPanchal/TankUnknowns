using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{

    public GameObject sourcePlayer;
    public Vector3 originalPosition;

    private float range = 6f;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, originalPosition) > range)
        {
            NetworkServer.Destroy(this.gameObject);
        }
	}
    void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.GetComponent<TankHealth>().TakeDamage(1, sourcePlayer);
        }        
        NetworkServer.Destroy(this.gameObject);
    }
}
