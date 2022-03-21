using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //health, speed, coin worth
    public int health = 3;
    public float speed = 3f;
    public int enemyCoins = 3;

    private Transform target;
    private int targetWaypointIndex = 1;

    


    // Start is called before the first frame update
    void Start()
    {
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
        
        Debug.Log(dotProduct);
        

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
            Destroy(gameObject);
            return;
        }
        
        targetWaypointIndex++;
        target = Waypoints.waypoints[targetWaypointIndex];
        transform.LookAt(target);
    }
    
    
    //Health
    public void TakeDamage(int hitAmount)
    {
        health -= hitAmount;
        
        if (health <= 0)
        {
            Economy.playerCoins += enemyCoins;
            Destroy(this.gameObject);
        }
    }
}
