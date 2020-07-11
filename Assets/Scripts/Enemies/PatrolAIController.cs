using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAIController : MonoBehaviour
{
    [SerializeField] Transform[] Route;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool paused = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (Route.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = Route[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % Route.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!paused)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }


    public void PauseRoute()
    {
        agent.SetDestination(this.transform.position);
        paused = true;
    }

    public void ResumeRoute()
    {
        paused = false;
    }


}