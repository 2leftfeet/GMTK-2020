using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMachine : MonoBehaviour
{
    bool isUsed = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!isUsed)
        {
            Destroy(collision.gameObject);
            //Sukurt Dissolve Effect
            isUsed = true;
        }
    }
}
