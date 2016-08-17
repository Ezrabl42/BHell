using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{

    private Rigidbody rb;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10.0f; //starts out at 10
    }

    void Update()
    {

    }

    void FixedUpdate() //using physics for moving
    {


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;
        //no Newtonian forces, just moving U/D/L/R


    }
}
