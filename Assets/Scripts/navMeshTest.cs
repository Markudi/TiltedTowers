using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshTest : MonoBehaviour
{


    public Transform target;


    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //todo: #3 - place enemey at cloestest navmesh point (create getnavmeshpointion)
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 meshPosition = GetNavmeshPosition(target.position);
        // // agent.SetDestination(target.position);
        // agent.SetDestination(meshPosition);
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            //todo 4: - raycast from the mouse and pick a new destination for the agent

            Ray pickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(pickRay, out RaycastHit hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }

        }
    }



    Vector3 GetNavmeshPosition(Vector3 samplePosition)
    {
        //todo #2 - place enemy at closest navmesh point (create getnavmeshposition)
        NavMesh.SamplePosition(samplePosition, out NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
        // return Vector3.zero;
    }
}
