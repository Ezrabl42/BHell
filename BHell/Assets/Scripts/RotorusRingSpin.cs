﻿using UnityEngine;
using System.Collections;
//Responsibilities:
//spin the ring.  That's it lol
public class RotorusRingSpin : MonoBehaviour {
    public float rotateSpeed;

    // Update is called once per frame

    void Update()
    {
        transform.Rotate(new Vector3(1.3f, 1, 1.7f) * rotateSpeed * Time.deltaTime);
        //turn <rotateSpeed> degrees each second

    }

}
