using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BobbingAnimation : MonoBehaviour
{
    [SerializeField] float bobbingFrequency = 1.0f;
    [SerializeField] float bobbingMagnitude = 1.0f; 

    float bobbingFrequencyOffset;
    float bobbingOffset;
    float oldBobbingOffset;

    bool hasReset = false;

    NavMeshAgent agent;

    void Awake()
    {   
        bobbingFrequencyOffset = 0.0f;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if((agent.transform.position - agent.destination).magnitude >= 0.5f)
        {
            hasReset = false;

            bobbingOffset = Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency + bobbingFrequencyOffset)) * bobbingMagnitude;
            agent.baseOffset += bobbingOffset - oldBobbingOffset;
            oldBobbingOffset = bobbingOffset;
        }
        else
        {
            if(!hasReset)
            {
                agent.baseOffset -= oldBobbingOffset;
                //transform.rotation = Quaternion.identity;
                hasReset = true;
                oldBobbingOffset = 0.0f;
            }
        }
    }
}
