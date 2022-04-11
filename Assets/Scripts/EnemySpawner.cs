using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };

    [System.Serializable]
    public class EnemyWave
    {
        public GameObject orcPrefab;
        public string waveName;
        public int amountOfEnemies;
        public float spawnRate;
    }

    public EnemyWave[] myEnemyWaves;
    public float timeBetweenWaves = 5f;
    public float waveCountdown = 0f;

    public GameManager myGamemanager;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    
    private int waveNumber = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        //Start spawning
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(myEnemyWaves[waveNumber]));
            }
        }
        else
        {
            //Decrease the wave count down every second
            waveCountdown -= Time.deltaTime;
        }
    }


    //After wave is completed go to next wave
    private void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        //Check if the next wave would be null, if it is then end the game.
        if (waveNumber + 1 > myEnemyWaves.Length - 1)
        {
            waveNumber = 0;
            
            myGamemanager.EndGameWon();
            //stop waveSpawner
            this.gameObject.SetActive(false);
        }
        else
        {
            waveNumber++;
        }
        
    }


    //Check if any enemies are still alive
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //Check if there are anymore enemies, if there is then don't send out next wave.
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        
        return true;
    }


    //Spawn Wave
    IEnumerator SpawnWave(EnemyWave enemyWave)
    {
        Debug.Log("Spawning wave: " + enemyWave.waveName);
        state = SpawnState.Spawning;

        //Spawn each enemy in the wave
        for (int i = 0; i < enemyWave.amountOfEnemies; i++)
        {
            SpawnEnemy(enemyWave.orcPrefab);
            yield return new WaitForSeconds(1f / enemyWave.spawnRate);
        }


        state = SpawnState.Waiting;
        
        yield break;
    }
    
    

    //Spawn Enemy
    void SpawnEnemy(GameObject myEnemy)
    {
        //Spawn the enemy
        Instantiate(myEnemy, transform.position, transform.rotation);
    }
    
    
    
}
