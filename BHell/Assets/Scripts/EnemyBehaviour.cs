using UnityEngine;
using System.Collections;
[System.Serializable]
public class Shooting //sibling class?????????????????????????
{
    public float aimTime, fireCharge, fireWait;   //time to turn, time to charge, time in between firing

    private float nextFire = 0.0f;//keep track of when next botlt is ready

    public float getNextFire()
    {
        return nextFire;
    }
    public void setNextFire(float next)
    {
        this.nextFire = next;
    }
}
public class EnemyBehaviour : MonoBehaviour
{
    public GameObject primaryProjectile;
    public Transform primaryFireSpawn;
    public Shooting primaryFire;
    

    public EnemyBehaviour()
    {

    }
}