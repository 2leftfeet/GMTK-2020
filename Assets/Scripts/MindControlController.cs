using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAgentInput
{
    void DoUpdate(NavMeshAgent agent);

    void Pause(NavMeshAgent agent);
    void Resume(NavMeshAgent agent);
}

public class MindControlController : MonoBehaviour
{
    [SerializeField] public Controllable startAgent;
    [SerializeField] IAgentInput input;

    MindControlEffect mindControl;
    LineRenderer affectedWire;
    public Controllable currentAgent;
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
                    NewAgent(potentialAgent);
                    /*if(affectedWire)
                    {
                        affectedWire.endColor = Color.white;
                        affectedWire = null;
                    }

                    currentAgent.input = currentAgent.defaultInput;
                    currentAgent.ResumeInput();
                    currentAgent = potentialAgent;
                    currentAgent.PauseInput();
                    currentAgent.input = input;

                    affectedWire = currentAgent.GetComponentInChildren<LineRenderer>();
                    if(affectedWire)
                    {
                        affectedWire.endColor = Color.green;
                    }
                    
                    if(ControlingEnemy()) mindControl.ControlEffect();
                    else mindControl.ControlEffectEnd();*/
                }
            }
        }
    }

    public void NewAgent(Controllable potentialAgent)
    {
        if(affectedWire)
        {
            affectedWire.endColor = Color.white;
            affectedWire = null;
        }

        currentAgent.input = currentAgent.defaultInput;
        currentAgent.ResumeInput();
        currentAgent = potentialAgent;
        currentAgent.PauseInput();
        currentAgent.input = input;

        affectedWire = currentAgent.GetComponentInChildren<LineRenderer>();
        if(affectedWire)
        {
            affectedWire.endColor = Color.green;
        }
        
        if(ControlingEnemy()) mindControl.ControlEffect();
        else mindControl.ControlEffectEnd();
    }
}
