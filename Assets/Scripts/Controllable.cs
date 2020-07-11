using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Controllable : MonoBehaviour
{
    NavMeshAgent agent;
    public IAgentInput input;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(input != null)
        {
            agent.destination = input.TargetPos;
        }
    }
}
