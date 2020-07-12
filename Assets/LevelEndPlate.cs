using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPlate : MonoBehaviour
{
    Transition transitionManager;
    private void Awake(){
        transitionManager = GameObject.Find("/TransitionManager").GetComponent<Transition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.Translate(new Vector3(0f, -0.05f, 0f));
            transitionManager.TransitionLevel();
        }
    }
}
