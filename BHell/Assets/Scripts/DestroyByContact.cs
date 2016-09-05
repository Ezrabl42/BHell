using UnityEngine;
using System.Collections;
//Behavior responsible for managing:
//Object collisions
//Collisions with projectiles and rays
//Destruction of objects

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion; //splosion goes here
    private Collider myCollider; //check our own collider


	void OnTriggerEnter(Collider other) //when the trigger is touched by anything
    {
        myCollider = GetComponent<Collider>();

        if(other.tag == "Boundary" || other.tag == gameObject.tag) //don't explode when hitting boundary and don't let objects of same class destroy each other.
        {
            return;
        }


     

     
        checkHitEnemy();
        checkHitPlayer();


        Instantiate(explosion, transform.position, transform.rotation); //make explosion

        Destroy(other.gameObject);
        Destroy(gameObject);      
    }


    void OnRayCastReceived() //hit by ray, this is called by message only when objects of different classes cast rays at each other
    {
        Instantiate(explosion, transform.position, transform.rotation); //make explosion
        checkHitEnemy();
        checkHitPlayer();
        Destroy(gameObject);
    }

    void checkHitPlayer()
    {
        if (gameObject.name == "PlayerShip") //check if destroyed object is the Player
        {
            GameObject gameController = GameObject.Find("GameController");
            gameController.SendMessage("endGame"); //end the game
        }

    }

    void checkHitEnemy()
    {
        if (gameObject.tag == "Enemy") //check if destroyed object is an Enemy
        {
            EnemyBehaviour enemyBehaviour = GetComponent<EnemyBehaviour>();
            enemyBehaviour.bounty.AwardBounty(); //tell the Enemy to award points
        }
    }

}
