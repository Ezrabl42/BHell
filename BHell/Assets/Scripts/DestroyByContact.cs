using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion; //splosion goes here
    private Collider myCollider; //check our own tag


	void OnTriggerEnter(Collider other) //when the trigger is touched by anything
    {
        myCollider = GetComponent<Collider>();

        if(other.tag == "Boundary" || other.tag == gameObject.tag) //don't explode when hitting boundary and don't let object's children kill the parent.
        {
            return;
        }


        Instantiate(explosion, transform.position, transform.rotation); //make explosion
        if(myCollider.tag == "Enemy") //check if destroyed object is Enemy Object
        {
            myCollider.gameObject.SendMessage("AwardBounty");
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
        
    }
    void OnRayCastReceived() //hit by ray
    {
        Instantiate(explosion, transform.position, transform.rotation); //make explosion
        Destroy(gameObject);
    }


}
