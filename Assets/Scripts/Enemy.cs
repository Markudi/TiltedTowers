using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    
    
    //health, speed, coin worth
    public int maxHealth = 3;
    public int currentHealth;
    public float speed = 3f;
    public int enemyCoins = 3;

    public HealthBar healthBar;


    public GameObject deathEffect;
    


    private Transform target;
    private int targetWaypointIndex = 0;

    


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxhealth(maxHealth);
        
        // start position
         transform.position = Waypoints.waypoints[0].position;
        
        //target transform
        target = Waypoints.waypoints[targetWaypointIndex];

    }

    
    // Update is called once per frame
    void Update()
    {
        // Proximity movement
        //     MoveEnemy();
        //
        // if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        // {
        //     TargetNextWaypoint();
        // }


        //Dotproduct apporach
        Vector3 myDirection = transform.forward;
        
        Vector3 waypointDirection = target.position - transform.position;
        
        float dotProduct = Vector3.Dot(myDirection, waypointDirection) ;
        
        if (dotProduct > 0)
        {
            MoveEnemy();
        }
        else if(dotProduct <= 0)
        {
            //my Attempt at clamping
            transform.position = new Vector3(Mathf.Clamp(target.position.x, target.position.x, target.position.x),
                Mathf.Clamp(target.position.y, target.position.y, target.position.y), Mathf.Clamp(target.position.z, target.position.z, target.position.z));
            //next target
            TargetNextWaypoint();
        }
        
        // Debug.Log(dotProduct);
        

    }

    
    //FUNCTIONS

    //Movement
    private void MoveEnemy()
    {
        Vector3 newPosition = transform.position;
        Vector3 movementDirection = (target.position - transform.position).normalized;
        newPosition += movementDirection * speed * Time.deltaTime;

        transform.position = newPosition;
    }
    
    
    //Target
    private void TargetNextWaypoint()
    {

        if (targetWaypointIndex >= Waypoints.waypoints.Length - 1)
        {
            DecreasePlayerLives();
            return;
        }
        
        targetWaypointIndex++;
        target = Waypoints.waypoints[targetWaypointIndex];
        transform.LookAt(target);
    }


    private void DecreasePlayerLives()
    {
        Economy.playerLives--;
        Destroy(gameObject);
    }
    
    
    //Health
    public void TakeDamage(int hitAmount)
    {
        currentHealth -= hitAmount;
        
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth <= 0)
        {
            Economy.playerCoins += enemyCoins;
            
            //Death partial system
            Instantiate(deathEffect, transform.position, transform.rotation);
            
            Destroy(this.gameObject);
            
            
            
        }
    }
}
