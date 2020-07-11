using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NullInput : MonoBehaviour, IAgentInput
{
    // Start is called before the first frame update
    public void DoUpdate(NavMeshAgent agent)
    {}

    public void Pause(NavMeshAgent agent)
    {}

    public void Resume()
    {}
}
