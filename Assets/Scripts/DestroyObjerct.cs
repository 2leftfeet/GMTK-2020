using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjerct : MonoBehaviour
{

    [SerializeField] float heightTopDestroy = -10f;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= heightTopDestroy)
            Destroy(gameObject);
    }
}
