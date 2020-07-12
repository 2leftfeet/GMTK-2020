using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAgentInput
{
    void DoUpdate(NavMeshAgent agent);

    void Pause(NavMeshAgent agent);
    void Resume();
}

public class MindControlController : MonoBehaviour
{
    [SerializeField] Controllable startAgent;
    [SerializeField] IAgentInput input;

    MindControlEffect mindControl;

    [HideInInspector] public Controllable currentAgent;
    Camera mainCamera;

    public bool ControlingEnemy()
    {
        return currentAgent!=startAgent;
    }


    void Start()
    {
        mainCamera = Camera.main;
        input = GetComponent<IAgentInput>();
        mindControl = GetComponent<MindControlEffect>();

        currentAgent = startAgent;
        currentAgent.input = input;
        //currentAgent.defaultInput = input;
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
                    currentAgent.input = currentAgent.defaultInput;
                    currentAgent.ResumeInput();
                    currentAgent = potentialAgent;
                    currentAgent.PauseInput();
                    currentAgent.input = input;
                    if(ControlingEnemy()) mindControl.ControlEffect();
                    else mindControl.ControlEffectEnd();
                }
            }
        }
    }
}
