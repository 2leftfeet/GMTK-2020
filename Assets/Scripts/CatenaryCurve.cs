using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatenaryCurve : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    [SerializeField] float slack;
    [SerializeField] int pointCount;
    List<Vector3> points;

    LineRenderer lineRenderer;

    float Asinh(float f)
    {
        return Mathf.Log(f + Mathf.Sqrt(f*f + 1), Mathf.Exp(1.0f));
    }

    float Catenary(float a, float x)
    {
        return a * (float)System.Math.Cosh( x / a);
    }

    float GetA(float vecLen, float maxLen)
    {
        float e = float.MaxValue;
        float a = 100.0f;
        float aTmp = 0.0f;
        float maxLenHalf = 0.5f * maxLen;
        float vecLenHalf = 0.5f * vecLen;

        int MAX_TRIES = 100;

        for(int i = 0; i < MAX_TRIES; i++)
        {
            aTmp = vecLenHalf / Asinh(maxLenHalf / a);
            e = Mathf.Abs((aTmp - a)/ a);
            a = aTmp;
            if(e < 0.001) break;
        }

        return a;
    }

    List<Vector3> GetPoints(Vector3 v0, Vector3 v1, float maxLen, int segmentCount)
    {
        
        float vecLen = (v1 - v0).magnitude;
        float vecLenHalf = vecLen * 0.5f;
        float segInc = vecLen / segmentCount;
        float A = GetA(vecLen, maxLen);
        float offset = Catenary(A, -vecLenHalf);

        List<Vector3> points = new List<Vector3>();
        points.Add(v0);

        Vector3 pnt;

        for(int i = 1; i < segmentCount; i++)
        {
            pnt = Vector3.Lerp(v0, v1, (float)i/segmentCount);
            float xpos = i * segInc - vecLenHalf;
            float c = offset - Catenary(A, xpos);

            pnt.y -= c;
            //pnt /= 2;
            points.Add(pnt);
        }
        points.Add(v1);

        return points;

    }

    void Start()
    {
        points = new List<Vector3>(pointCount);
        lineRenderer = GetComponent<LineRenderer>();
        
    }

    void Update()
    {

        points = GetPoints(point1.position, point2.position, slack, pointCount);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    
}
