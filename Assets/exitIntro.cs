using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class exitIntro : MonoBehaviour
{
    Controllable controllable;
    NavMeshAgent NavMeshAgent;
    BobbingAnimation bobbing;
    // Start is called before the first frame update
    void Start()
    {
        controllable = GetComponent<Controllable>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        bobbing = GetComponent<BobbingAnimation>();
    }

    public void ExitIntro()
    {
        controllable.enabled = true;
        NavMeshAgent.enabled = true;
        bobbing.enabled = true;
        Destroy(this);
    }
}
