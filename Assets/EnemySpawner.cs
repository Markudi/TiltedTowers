using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemeyToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnEnemy()
    {
        Instantiate(enemeyToSpawn);
        
        Invoke("SpawnEnemy", 4f);
    }
    
    
}
