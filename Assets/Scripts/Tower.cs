using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Tower : MonoBehaviour
{


    //Target
    private Transform target;
    [Header("Targeting")]
    
    public float range = 15f;
    public string enemyTag = "Enemy";
    
    //Tower rotation variables
    [Header("Tower Rotation")]
    public float rotationSpeed = 10f;
    //rotating top of tower
    public Transform partToRotate;
    
    //Tower shooting variables
    [Header("Shooting")]
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    [Header("Arrow")]
    public GameObject arrowPrefab;
    public Transform firePoint;
    



    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
        //list of enemies type "enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
        float shortestDistance = Mathf.Infinity; 
        GameObject nearestEnemy = null;

        //find closest enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //target becomes enemy within range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }


        //-----Tower Rotation------
        //Find direction we want to look at
        Vector3 dir = target.position - transform.position;
        //get the rotation of that direction and convert
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 towerRotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        //make our model rotate based on the y-rotation to that direction
        partToRotate.rotation = Quaternion.Euler(0f,towerRotation.y, 0f);
        
        //-----Tower shooting------
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;

    }


    private void Shoot()
    {
       GameObject arrowGameObject = (GameObject)Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
       Arrow arrow = arrowGameObject.GetComponent<Arrow>();

       if (arrow != null)
       {
           arrow.Seek(target);
       }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
