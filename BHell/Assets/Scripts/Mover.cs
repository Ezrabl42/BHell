using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{

    private Rigidbody rb;

    public float bulletSpeed;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
