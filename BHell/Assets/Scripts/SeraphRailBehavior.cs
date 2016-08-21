using UnityEngine;
using System.Collections;
//Behavior for the SeraphRail ships
//Ships will "flip" into the screen as if they came from underneath the game area
//They will rotate to face the player slowly
//After some set fire time, they will fire a laser, and then continue to rotate until the next bolt is ready.


public class SeraphRailBehavior : EnemyBehaviour //child of EnemyBehaviour class
{
    //private variables
    private Rigidbody rb;              //rigidbody
    private Quaternion setupOrientation; //enemy will "rotate in"
    private Quaternion aimRotation; //hold a target rotation
    private Transform targetTransform;
    //flag for proper setup
    private bool readyUp = false;
    //aiming variables
    private float stopAiming = 0.0f; //stop aiming at this time
    private float stopCharging = 0.0f; //stop charging at this time
    public float aimTime;  //how long we can aim
    //ray casting
    private LineRenderer line;
    // Use this for initialization



    void Start()
    {
        //get rigidbody ready, find the player, set up the transforms to face
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("PlayerShip");  //we will never aim at something that isn't the player
        targetTransform = target.transform;

        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;


        //determine our desired orientation
        setupOrientation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(RotateIn(setupOrientation)); //start doing this

        //find the Quaternion that will face the player
        aimRotation = Quaternion.LookRotation(targetTransform.position - transform.position);
        
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
            StopCoroutine("RotateIn"); //we don't have to do this anymore
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation, Time.deltaTime / aimTime);

        }

        else if (Time.time >= stopAiming && Time.time < stopCharging)
        {



        }
        else if (Time.time >= stopCharging && Time.time < primaryFire.getNextFire())
        {
            line.enabled = true;
          
            line.material.mainTextureOffset = new Vector2(0, Time.time * 10); //spinning effect :D
            Ray ray = new Ray(transform.position, transform.forward); //make new ray
            RaycastHit hit; //holds what ever was impacted
            line.SetPosition(0, ray.origin); //first end of the line

            if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Player")
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

            aimRotation = Quaternion.LookRotation(targetTransform.position - transform.position); //create aiming Rotation
        }
    }
}



