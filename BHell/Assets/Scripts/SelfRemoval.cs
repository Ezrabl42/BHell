using UnityEngine;
using System.Collections;

public class SelfRemoval : MonoBehaviour
{

    public float deathTime; //set the timer     

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, deathTime);  //simply kill this object after a set time.
	}
	
	
}
