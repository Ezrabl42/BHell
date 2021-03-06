﻿using UnityEngine;
using System.Collections;
//Behavior for the SeraphRail ships
//Ships will "flip" into the screen as if they came from underneath the game area
//They will rotate to face the player slowly
//After some set fire time, they will fire a laser, and then continue to rotate until the next bolt is ready.


public class SeraphRailBehavior : EnemyBehaviour //child of EnemyBehaviour class
{
    //transformation variables
    private Quaternion setupOrientation;    //enemy will "rotate in" until it matches this
    private Quaternion aimRotation;         //hold a target rotation
    
    //flag for proper setup
    private bool readyUp = false;

    //aiming variables
    private float stopAiming = 0.0f;        //stop aiming at this time
    private float stopCharging = 0.0f;      //stop charging at this time
    public float aimTime;                   //how long we can aim

    //ray casting
    private LineRenderer line;

    //audio
    private AudioSource fireSound;
    /****************************************************************************************************/


    // Use this for initialization
    void Start()
    {
        //get rigidbody ready, find the player, set up the transforms to face
     
        target = GameObject.Find("PlayerShip");  //Seraphs will never aim at something that isn't the player
        if(target == null)
        {
            target = GameObject.Find("GameController"); //this is a cheat to aim at origin and prevent errors when the player is gone
        }
        

        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;                    //No firing yet

        fireSound = GetComponent<AudioSource>();

        //determine our desired orientation and turn until facing it
        setupOrientation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(RotateIn(setupOrientation));


        //find the Quaternion that will face the player for the first time
        aimRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        StopCoroutine("RotateIn"); //we don't have to do this anymore once it's finished
    }

    // Update is called once per frame
    void Update()
    {
        if (readyUp == true) //only do this if we finished doing the setup!
        {
            FireCycle();
        }
    }

    IEnumerator RotateIn(Quaternion end)
    {
        while (transform.rotation != end)
        {
            transform.Rotate(new Vector3(1, 0, 0)); //rotate 1 degrees per frame until we reach desired orientation
            yield return 0;
        }
        readyUp = true;

    }

    void FireCycle() //deals with the firing cycle (aiming, charging, firing)
    {
        if (Time.time < stopAiming)
        {          
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation,  Time.deltaTime / aimTime);
        }

        else if (Time.time >= stopAiming && Time.time < stopCharging)
        {
        }
        
        else if (Time.time >= stopCharging && Time.time < primaryFire.getNextFire())
        {
            if(Time.time >= stopCharging && Time.time <= stopCharging + 0.02f)
            {
                fireSound.Play();
            }
            line.enabled = true;
            
            line.material.mainTextureOffset = new Vector2(0, Time.time * 10); //spinning effect :D
            Ray ray = new Ray(transform.position, transform.forward); //make new ray
            RaycastHit hit; //holds what ever was impacted
            line.SetPosition(0, ray.origin); //first end of the line

            if (Physics.Raycast(ray, out hit, 100) && hit.collider.name == "PlayerShip")
            {     
                line.SetPosition(1, hit.point);
                hit.collider.gameObject.SendMessage("OnRayCastReceived");             
            }

            else
            {
                line.SetPosition(1, ray.GetPoint(100));  //100 units forward :D
            }
        }
        else
        {
            line.enabled = false;
            stopAiming = Time.time + aimTime;
            stopCharging = stopAiming + primaryFire.fireWait;
            primaryFire.setNextFire(stopCharging + primaryFire.fireDuration);  //Total time alloted is aimTime + fireDuration + fireWait

            target = GameObject.Find("PlayerShip");  //Seraphs will never aim at something that isn't the player
            if (target == null)
            {
                target = GameObject.Find("GameController"); //this is a cheat to aim at origin and prevent errors when the player is gone
            }
            aimRotation = Quaternion.LookRotation(target.transform.position - transform.position); //create a new aiming Rotation
        }
    }
}



