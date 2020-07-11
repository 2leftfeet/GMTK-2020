using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public event Action onActivate = delegate { };
    public event Action onDeactivate = delegate { };

    [SerializeField] Color inactive;
    [SerializeField] Color active;
    

    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Character"))
        {
            
            mesh.material.SetColor("_BaseColor",active);
            mesh.material.SetColor("_EmmisionColor",active);
            transform.Translate(new Vector3(0f, -0.05f, 0f));
            onActivate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            mesh.material.SetColor("_BaseColor", inactive);
            mesh.material.SetColor("_EmmisionColor", inactive);
            transform.Translate(new Vector3(0f, 0.05f, 0f));
            onDeactivate();
        }
    }



}
