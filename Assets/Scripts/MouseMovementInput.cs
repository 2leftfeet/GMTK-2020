using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementInput : MonoBehaviour, IAgentInput
{
    Camera mainCamera;

    public Vector3 TargetPos {get; private set;}

    void Start()
    {
        mainCamera = Camera.main;
        //TargetPos = Vector3.zero;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                TargetPos = hit.point;
            }
        }
    }

    public void ResetTargetPos(Vector3 basePos)
    {
        TargetPos = basePos;
    }
}
