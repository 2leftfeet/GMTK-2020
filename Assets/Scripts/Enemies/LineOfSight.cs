using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class LineOfSight : MonoBehaviour
{

    [SerializeField] Transform parent;

    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    
    [SerializeField] private float fov = 90f;
    [SerializeField] float viewDistance = 5f;
    [SerializeField] int rayCount = 50;
    [SerializeField] float textureTileSize = 5;



    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }


    private void LateUpdate()
    {
        SetAimDirection(-parent.forward);
        SetOrigin(parent.position);

        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        
        for (int i = 0; i <= rayCount; i++)
        {
            RaycastHit raycastHit;
            Vector3 vertex;

            Physics.Raycast(origin, GetVectorFromAngle(angle), out raycastHit, viewDistance);

            if (raycastHit.transform.CompareTag("Player"))
            {
                // gameManager.PlayerDetected();
            }

            if (raycastHit.collider == null)
            {
                // nohit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //hit   
                vertex = raycastHit.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 rotetedVertex = parent.transform.rotation * vertices[i];
            uv[i] = new Vector2(rotetedVertex.x / textureTileSize, rotetedVertex.z / textureTileSize);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromFloat(aimDirection) - fov / 2f;
    }

    public void SetViewDistance(float distance)
    {
        viewDistance = distance;
    }

    public void SetFieldOfView(float newFOV)
    {
        fov = newFOV;
    }

    #region formulas
    Vector3 GetVectorFromAngle(float angle)
    {
        // 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0 , Mathf.Sin(angleRad));
    }

    float GetAngleFromFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(-dir.x, dir.z) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    #endregion

}
