using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowers : MonoBehaviour
{
    public GameObject tower1Prefab;
    public int towerCost  = 2;
    
    private Camera mainCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            SpawnTower();
        }
        

    }


    private void SpawnTower()
    {

        if (Economy.playerCoins < towerCost)
        {
            Debug.Log("You do not have enough money!");
        }

        //If player has more than 0 coins spawn tower.
        if (Economy.playerCoins >= towerCost)
        {
            //My ray
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
            //Spawn tower where my ray hits, decrease player coins by cost.
            if (Physics.Raycast(ray, out RaycastHit mouseHit))
            {
                //Can only place towers on "placeable" area also cannot place towers on other towers.
                if (!mouseHit.collider.CompareTag("CannotPlace") && mouseHit.collider.CompareTag("Placeable"))
                {
                    Instantiate (tower1Prefab,mouseHit.point,Quaternion.identity);
                    Economy.playerCoins -= towerCost;
                }
                else
                {
                    Debug.Log("You cannot build in this location!");
                }

            }
            
        }


        
    }
    
}
