using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // game over 
    public bool gameWon;
    GameOver gameover;

    public Text waveText;
    public Text endwaveMessage;
    
    // variable used to keep track of spawned health potions on screen at 1 time.
    public int HPspawned = 0;
    // variable used to only have 1 fuel spawned at 1 time
    public int fuelSpawned = 0;

    // array of spawn points for fuel
    public GameObject [] fuelPositions;

    // fuel gameobject
    public GameObject fuel;

    // array of spawn points for zombies
    public GameObject [] spawners;

    // zombie gameobject
    public GameObject zombie;

    // zombie spawning variables
    public int wave;
    public int maxZombies;
    public int spawnedZombies;
    public int zombiesAlive;
    public float zomHealthIncrease;
    private float spawnTimer;

    // gameover statistic variables
    public int totalSouls;
    public int totalFuel;
    public float totalHeadshots;
    public float totalBodyshots;
    public float totalShotsfired;
    public float totalAccuracy;

    // singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject); // immortal object
    }

    // Use this for initialization
    private void Start () {
        gameWon = false;

        gameover = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameOver>();
        fuelPositions = GameObject.FindGameObjectsWithTag("Fuel");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        waveText.text = "WAVE: 1";

        wave = 1;
        maxZombies = 5;
        spawnedZombies = 0;
        zombiesAlive = 0;
        zomHealthIncrease = 0f;
        spawnTimer = 0f;

        // game over variables
        totalSouls = 0;
        totalFuel = 0;
        totalHeadshots = 0f;
        totalBodyshots = 0f;
        totalShotsfired = 0f;
        totalAccuracy = 0f;
}

    private void SpawnFuel ()
    {
        int randomSpawner = Random.Range(0, fuelPositions.Length);
        Instantiate(fuel, fuelPositions[randomSpawner].transform.position, fuelPositions[randomSpawner].transform.rotation);
        fuelSpawned++;
    }

    // new spawners become active
    public void UpdateActiveSpawners ()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    private void SpawnZombie ()
    {
        int randomSpawner;  // store chosen spawner

        // check player is not overlapping spawner to avoid enemy spawning inside of player
        if (spawners.Length > 1) // avoid infinite loop
        {
            SpawnerOverlap so; 

            do
            {
                randomSpawner = Random.Range(0, spawners.Length); // exlusive of max value
                so = spawners[randomSpawner].GetComponent<SpawnerOverlap>(); // get script from chosen spawn location

            } while (so.isOverlapping == true); // keep looping while player is inside the chosen spawner
        }
        else // if only 1 spawner exists
        {
            randomSpawner = Random.Range(0, spawners.Length);
        }

        Vector3 randomSpawnPoint = spawners[randomSpawner].transform.position;
        Instantiate(zombie, randomSpawnPoint, spawners[randomSpawner].transform.rotation);
        spawnedZombies++; // increment spawned zombies
        zombiesAlive++; // increment number of zombies alive
    }

    // zombie died
    public void DecreaseZombieCount ()
    {
        zombiesAlive--; // decrease number of zombies alive
    }

    public void GameOver()
    {
        totalAccuracy = (totalHeadshots + totalBodyshots) / totalShotsfired * 100; // calculate weapon accuracy
        gameover.FadeOut();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (!gameWon) // while the game has not been completed
        {
            // spawn zombies -- codes snipped & modified from https://www.youtube.com/watch?v=S1lZyKI384Y
            if (spawnedZombies < maxZombies) // while max amount of zomibes has not been spawened
            {
                if (zombiesAlive < 13) // to stop player being overwhelmed, limit to 13 at any one time.
                {
                    if (spawnTimer > 3f) // every 3 seconds spawn zombie
                    {
                        SpawnZombie();
                        spawnTimer = 0f; // reset spawn timer
                    }
                    else
                    {
                        spawnTimer += Time.deltaTime; // count in seconds
                    }
                }
            }
            else // next wave
            {
                if (zombiesAlive == 0) // all zombies dead in  round
                {
                    WaveEnded(); // show end wave message
                    wave++;
                    maxZombies += wave; // increase amount of zombies per round
                    spawnedZombies = 0; // reset amount of zombies spawned for new round
                    zomHealthIncrease += 10f; // make zombie progressively harder to kill
                    spawnTimer = -7f; // short break before next round

                    waveText.text = "WAVE: " + wave;
                }
            }

            if (fuelSpawned == 0) // only spawn 1 fuel at a time in the map
            {
                SpawnFuel();
            }
        }
	}

    public void WaveEnded()
    {
        endwaveMessage.text = "WAVE " + wave + " COMPLETE \n NEXT WAVE BEGINS IN 10 SECONDS";
        endwaveMessage.enabled = true;
        StartCoroutine("ShowWaveMessage");
    }

    IEnumerator ShowWaveMessage()
    {
        yield return new WaitForSeconds(3.0f);
        endwaveMessage.enabled = false;
    }
}
