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

    [SerializeField]
    string animationToOpen = "";
    [SerializeField]
    string animationToClose = "";

    Animator animation;

    [SerializeField]
    bool hasToBeHeld = false;

    [SerializeField]
    bool doesReset = false;

    [SerializeField]
    float resetTimer = 1f;
    bool activated = false;
    bool activeCounter = false;

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
            activated = true;
            if (doesReset && !activeCounter)
            {
                activeCounter = true;
                StartCoroutine(ResetTimer(resetTimer));
            }
        }
        else if (hasToBeHeld)
        {
            animation.SetTrigger(animationToClose);
            activated = false;
        }
    }


    void CountActive() {
        totalActive++;
    }


    void CountDeactive() {
        totalActive--;
    }

    IEnumerator ResetTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (totalActive != totalPlates)
        {
            activated = false;
            animation.SetTrigger(animationToClose);
        }
        else
        {
            ResetTimer(time);
        }
        activeCounter = false;
    }
}
