using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Camera mainCamera;
    Rigidbody body;
    void Start()
    {
        mainCamera = Camera.main;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Vector3 moveVector = Vector3.ClampMagnitude(inputVector, 1.0f);

        Quaternion yRotation = Quaternion.Euler(0.0f, mainCamera.transform.rotation.eulerAngles.y, 0.0f);
        moveVector = yRotation * moveVector;

        body.velocity = moveVector * speed;
    }
}
