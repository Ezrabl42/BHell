using UnityEngine;
using System.Collections;

public class OutOfBoundsDestruction : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject); //destroy the stuff when it leaves the game area
        //NOTE: the player object itself is bounded within the box for this collider
        //Must manage both this boundary and the bounds applied to player object
    }
}
