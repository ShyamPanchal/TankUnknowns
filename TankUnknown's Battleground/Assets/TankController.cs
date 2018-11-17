using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {
    Rigidbody rb;
    public int speed;
    Vector3 m_EulerAngleVelocity;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        rb.velocity = transform.forward * y * speed;
        m_EulerAngleVelocity = new Vector3(0, x*100, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
