using UnityEngine;
using System.Collections;
//Responsibilities:
//spin the barrier.  That's it lol

public class RotorusBarrierBehavior : MonoBehaviour {

    public float rotateSpeed;

	// Update is called once per frame
   
	void Update ()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
        //turn <rotateSpeed> degrees each second
     
    }
    
}
