using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Controllable : MonoBehaviour
{
    NavMeshAgent agent;
    public bool startWithDefault = true;
    public IAgentInput defaultInput;
    
    public IAgentInput input;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultInput = GetComponent<IAgentInput>();
        if(startWithDefault)
            input = defaultInput;
    }

    void Update()
    {
        if(input != null)
        {
            input.DoUpdate(agent);
        }
    }

    public void PauseInput()
    {
        input.Pause(agent);
    }

    public void ResumeInput()
    {
        input.Resume(agent);
    }
}
