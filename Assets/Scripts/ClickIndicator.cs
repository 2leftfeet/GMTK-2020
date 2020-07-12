using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickIndicator : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    Vector3 destination;

    public void UpdateDestination(NavMeshAgent agent)
    {
        destination = agent.destination;
        SpawnIndicator();
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
