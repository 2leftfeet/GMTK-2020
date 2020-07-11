using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMovementInput : MonoBehaviour, IAgentInput
{
    Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        //TargetPos = Vector3.zero;
    }

    public void DoUpdate(NavMeshAgent agent)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    public void Pause(NavMeshAgent agent)
    {
        agent.destination = agent.gameObject.transform.position;
    }

    public void Resume()
    {}
}
