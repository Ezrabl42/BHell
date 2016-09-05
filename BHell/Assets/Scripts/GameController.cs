using UnityEngine;
using UnityEngine.SceneManagement; //IMPORTANT FOR GAME CONTROL
using System.Collections;
//GameController responsibilties
//******************************
//spawn waves
//track scoring
//handle starting, ending, and restarting the game


public class GameController : MonoBehaviour
{
    public GameObject hazard; //choosing a hazard.  Will need an array later so we can randomize this

    public float spawnWait;  //wait time between spawning
    public float startWait; //wait time at start
    public float waveWait;  //wait time between waves


    public int maxHazardSpawnCount; //maximum number of hazards per wave  

    public GUIText scoreText; //score text
    public GUIText restartText; //restart text
    public GUIText gameOverText; //game over text

    private bool isGameOver;
    private bool isRestarted;


    private int score;   //actual score
    //public float multiplier << :D

   
	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(600, 900, false); //set the window boundaries when we start.

        isGameOver = false;
        isRestarted = false;
        restartText.text = "";
        gameOverText.text = "";


        score = 0; //the score is 0;
        UpdateScore();
        StartCoroutine( SpawnWaves()); //spawning is a coroutine                  
	}
    void Update()
    {
        if(isRestarted)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //if restarting, then reload the active scene.
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
       
         while(true)
        {

            int hazardSpawnCount = Random.Range(1, maxHazardSpawnCount);


            for (int i = 0; i < hazardSpawnCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-28, 28), 0, Random.Range(33, 83));  //some defined spawn area
                Quaternion spawnRotation = hazard.transform.rotation;  //whatever the set rotation was 
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);  //wait between each spawn
            }
            yield return new WaitForSeconds(waveWait);

            if (isGameOver == true)
            {
                restartText.text = "Play again?\nPress 'R' to Restart.";
                isRestarted = true;
                break;
            }

        }

    }

    protected void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString("D8");
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateScore();
    }

    public void endGame()
    {
        gameOverText.text = "Ship Destroyed!\nGame Over.";
        isGameOver = true;
    }

}
