using UnityEngine;
using System.Collections;
//Behavior for the SeraphRail ships
//Ships will "flip" into the screen as if they came from underneath the game area
//They will rotate to face the player slowly
//After some set fire time, they will fire a laser, and then continue to rotate until the next bolt is ready.

public class SeraphRailBehavior : EnemyBehaviour {

    private Quaternion setupOrientation; //enemy will "rotate in"
    private GameObject target;
    private Transform targetTransform;

    private bool readyUp = false;




	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("PlayerShip");  //we will never aim at something that isn't the player
        targetTransform = target.transform; 

        setupOrientation = Quaternion.Euler(0, 180, 0);  //end orientation when we align to game area
        StartCoroutine(RotateIn(transform.rotation, setupOrientation)); //start doing this
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (readyUp == true && Time.time > primaryFire.getNextFire())
        {

        }
        
   
    }

    IEnumerator RotateIn(Quaternion start, Quaternion end)
    {
        while (transform.rotation != setupOrientation)
        {
            transform.Rotate(new Vector3(1, 0, 0)); //rotate 1 degrees per frame until we reach desired orientation
            yield return 0;
        }
        readyUp = true;
        
    }

    
    

}
