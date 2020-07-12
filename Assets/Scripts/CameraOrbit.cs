using UnityEngine;
using System.Collections;
 
public class CameraOrbit : MonoBehaviour
{
    public float multiplier = 2f;
    private Vector3 currentMouse;
    private Vector3 mouseDelta;
    private Vector3 lastMouse;

    
    [SerializeField] float minFov = 1f;
    [SerializeField] float maxFov = 5f;
    [SerializeField] float zoomSensitivity = 2f;
 
    public static bool activeRotate = false;
    Vector3 pivotPoint;

    public Transform target;
    Vector3 camLocalEulerAngles;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        //camLocalEulerAngles = transform.localEulerAngles;
        camLocalEulerAngles.x = 45;
        pivotPoint = new Vector3(target.position.x, target.position.y, target.position.z);
    }
 
    private void Update()
    {
        //camLocalEulerAngles.x = 45;
        mouseDelta = lastMouse - currentMouse;
 
        if (Input.GetMouseButtonDown(2))
        {
            activeRotate = true;
        }
 
 
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            activeRotate = false;
        }
 
        currentMouse = Input.mousePosition;
 
        mouseDelta = lastMouse - currentMouse;

        float fov = cam.orthographicSize;
        fov += Input.GetAxis("Mouse ScrollWheel") * -zoomSensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.orthographicSize = fov;
    }
 
 
    void LateUpdate ()
    {
        if (activeRotate)
        {
            transform.RotateAround(pivotPoint, Vector3.up, mouseDelta.x * -.1f * multiplier);
            //transform.RotateAround(pivotPoint, transform.right, mouseDelta.y * .1f * multiplier);
        }
 
        lastMouse = Input.mousePosition;
    }

}