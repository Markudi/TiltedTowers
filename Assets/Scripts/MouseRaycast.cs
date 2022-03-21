using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{

    public int hitDamage = 1;


    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            DamageEnemy();
        }
        
    }

    private void DamageEnemy()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit mouseHit))
        {
            //Get Enemy component on mouseHit object
            Enemy enemyTarget =  mouseHit.transform.GetComponent<Enemy>();
            //If enemyTarget is a type Enemy then do damage
            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(hitDamage);
            }
        }
    }
}
