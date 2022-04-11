using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    


    private Transform target;

    [Header("Arrow Stats")]
    public float speed = 70f;
    public int hitDamage = 1;

    public void Seek(Transform myTarget)
    {
        target = myTarget;
    }
    
    private void Update()
    {

        //If you have no target, destory arrow
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //get the direction of the enemy
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //If the arrow will actually hit the enemy, do damage to the enemy
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        //Have arrow rotated properly
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    private void HitTarget()
    {
        //Make sure hit target has a Enemy component
        Enemy enemyTarget =  target.transform.GetComponent<Enemy>();
        //If enemyTarget is a type Enemy then do damage
        if (enemyTarget != null)
        {
            enemyTarget.TakeDamage(hitDamage);
        }
        
        //Destory arrow
        Destroy(gameObject);
    }
    
    
}
