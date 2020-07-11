using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public event Action onActivate = delegate { };
    public event Action onDeactivate = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position.Set(transform.position.x, transform.position.y -0.1f, transform.position.z);
            onActivate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position.Set(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            onDeactivate();
        }
    }



}
