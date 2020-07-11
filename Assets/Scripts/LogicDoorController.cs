using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class LogicDoorController : MonoBehaviour
{
    [SerializeField]
    PressurePlateController[] pressurePlates;

    int totalActive = 0;
    int totalPlates;
    string animationToOpen = "";
    string animationToClose = "";
    Animator animation;
    bool hasToBeHeld = false;

    private void Awake()
    {
        animation = GetComponent<Animator>();

        totalPlates = pressurePlates.Length;

        foreach (PressurePlateController e in pressurePlates)
        {
            e.onActivate += CountActive;
            e.onDeactivate += CountDeactive;
        }
    }

    private void Update()
    {
        if(totalActive == totalPlates)
        {
            animation.SetTrigger(animationToOpen);
        }
        else if (hasToBeHeld)
        {
            animation.SetTrigger(animationToClose);
        }
    }


    void CountActive() {
        totalActive++;
    }


    void CountDeactive() {
        totalActive--;
    }

}
