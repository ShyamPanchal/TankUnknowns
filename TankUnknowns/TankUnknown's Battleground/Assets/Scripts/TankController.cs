using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankController : NetworkBehaviour {
    Rigidbody rb;
    public int speed;
    Vector3 m_EulerAngleVelocity;

    public GameObject mainCamera;
    public GameObject spotLight;

    public GameObject mapCamera;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        mapCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(hasAuthority == false)
        {
            return;
        }
        mainCamera.SetActive(true);
        spotLight.SetActive(true);
        mapCamera.SetActive(false);

        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        rb.velocity = transform.forward * y * speed;
        m_EulerAngleVelocity = new Vector3(0, x*100, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
