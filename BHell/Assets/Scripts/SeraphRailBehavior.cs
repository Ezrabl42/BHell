using UnityEngine;
using System.Collections;

public class SeraphRailBehavior : MonoBehaviour {

    
    private Quaternion setupOrientation; //enemy will "turn in"
   


	// Use this for initialization
	void Start ()
    {
        
        setupOrientation = Quaternion.Euler(0, 180, 0);
        
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(RotateIn(
            transform.rotation,
            setupOrientation));
     
        
    }

    IEnumerator RotateIn(Quaternion start, Quaternion end)
    {
        
        while(transform.rotation != setupOrientation)
        {
            
            transform.Rotate(new Vector3(10, 0, 0) * Time.deltaTime);
            yield return 0;
        }
        


    }

}
