using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion; //splosion goes here

	void OnTriggerEnter(Collider other) //when the trigger is touched by anything
    {
        if(other.tag == "Boundary" || other.tag == gameObject.tag) //don't explode when hitting boundary and don't let object's children kill the parent.
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation); //make explosion


        Destroy(other.gameObject);
        Destroy(gameObject);
        
    }


}
