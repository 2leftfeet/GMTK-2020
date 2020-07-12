using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMeniuCamera : MonoBehaviour
{
    [SerializeField] Transform MeniuCameraPosition;
    [SerializeField] Transform GameCameraPosition;
    [SerializeField] Transform Camera;

    bool inMeniu = true;
    float camPanDuration = 1f;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToggleCamera()
    {
        if (inMeniu)
        {
            inMeniu = false;
            StartCoroutine(LerpToPosition(camPanDuration, GameCameraPosition.position, false));
        }
        else
        {
            inMeniu = true;
            StartCoroutine(LerpToPosition(camPanDuration, MeniuCameraPosition.position, false));
        }
    }

    IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition, bool useRelativeSpeed = false)
    {
        
        if (useRelativeSpeed)
        {
            float totalDistance = GameCameraPosition.position.x - MeniuCameraPosition.position.x;
            float diff = Camera.transform.position.x - MeniuCameraPosition.position.x;
            float multiplier = diff / totalDistance;
            lerpSpeed *= multiplier;
        }

        float t = 0.0f;
        Vector3 startingPos = Camera.transform.position;

        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            Camera.transform.position = Vector3.Lerp(startingPos, newPosition, t);
            yield return 0;
        }
    }



}
