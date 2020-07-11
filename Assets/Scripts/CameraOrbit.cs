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
    float sensitivity = 0.5f;
 
    public static bool activeRotate = false;
    Vector3 pivotPoint; //

    public Transform target;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        pivotPoint = new Vector3(target.position.x, target.position.y, target.position.z);
    }
 
    private void Update()
    {
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
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.orthographicSize = fov;
    }
 
 
    void LateUpdate ()
    {
        if (activeRotate)
        {
            transform.RotateAround(pivotPoint, Vector3.up, mouseDelta.x * -.1f * multiplier);
            transform.RotateAround(pivotPoint, transform.right, mouseDelta.y * .1f * multiplier);
        }
 
        lastMouse = Input.mousePosition;
    }

}