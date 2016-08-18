using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}

public class PlayerController: MonoBehaviour // ':' means 'inheriting from'
{
    public Boundary bounds; //use the class we defined here!

    private Rigidbody rb; //we'll use the rigid body for the velocity
    private Quaternion endRotation; //Store target orientation for banking here!

    public float speed; //edit these in the inspector
    public float tilt; 
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
    }

    void FixedUpdate() //using physics for moving
    { 
        float moveHorizontal = Input.GetAxis("Horizontal"); //these two will be floats between 0 and 1
        float moveVertical = Input.GetAxis("Vertical"); //thus the in-game speed will be dictated by map size

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);  //the vector that points in the direction in which we wish to move

        rb.velocity = movement * speed; //a speed of around 100 will do for a map this size
        //no Newtonian forces, just moving U/D/L/R

        rb.position = new Vector3 //We have to stay within the box, so we keep the position within the set bounds
        (
            Mathf.Clamp(rb.position.x, bounds.xMin, bounds.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, bounds.zMin, bounds.zMax)
        ); 
        
        endRotation = Quaternion.AngleAxis(-tilt * (rb.velocity.x)/100f, transform.forward);  //we scale down the speed to be between 0 and 1
        //the tilt can be expressed as degrees.  tilt is inverted because transformations are by default clockwise
        rb.rotation = endRotation;
    }
}
