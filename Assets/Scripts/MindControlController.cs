using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAgentInput
{
    Vector3 TargetPos{ get;}
    void ResetTargetPos(Vector3 basePos);
}

public class MindControlController : MonoBehaviour
{
    [SerializeField] Controllable startAgent;
    [SerializeField] IAgentInput input;

    Controllable currentAgent;
    Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        input = GetComponent<IAgentInput>();

        currentAgent = startAgent;
        currentAgent.input = input;
        input.ResetTargetPos(currentAgent.transform.position);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)){
                var potentialAgent = hit.collider.gameObject.GetComponent<Controllable>();
                if(potentialAgent != null && potentialAgent != currentAgent)
                {
                    currentAgent.input = null;
                    currentAgent = potentialAgent;
                    currentAgent.input = input;
                    input.ResetTargetPos(currentAgent.transform.position);
                }
            }
        }
    }
}
