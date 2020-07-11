using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickIndicator : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    NavMeshAgent agent;
    Vector3 destination;
    public IAgentInput input;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            destination = agent.destination;
            SpawnIndicator();
        }
    }

    void SpawnIndicator()
    {
        if(GameObject.FindGameObjectsWithTag("Indicator").Length == 0)
        {
            Instantiate(indicator, destination,Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectsWithTag("Indicator")[0]);
            Instantiate(indicator, destination,Quaternion.identity);
        }
    }
}
