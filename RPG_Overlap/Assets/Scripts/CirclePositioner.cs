using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePositioner : MonoBehaviour
{
    public CircleRenderer circle1;
    public CircleRenderer circle2;
    public Material overlapMaterial;

    void Update()
    {
        if (circle1 != null && circle2 != null)
        {
            CreateOverlapMesh();
        }
    }
    void CreateOverlapMesh()
    {
        Vector3 center1 = circle1.transform.position;
        Vector3 center2 = circle2.transform.position;

        float radius1 = circle1.radius;
        float radius2 = circle2.radius;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        int segments = 100;
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 point1 = center1 + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius1;
            Vector3 point2 = center2 + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius2;

            if (Vector3.Distance(point1, center2) <= radius2)
            {
                vertices.Add(point1);
                if(vertices.Count > 1)
                {
                    triangles.Add(0);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                }
            }

            if (Vector3.Distance(point2, center1) <= radius1)
            {
                vertices.Add(point2);
                if(vertices.Count > 1)
                {
                    triangles.Add(0);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        GameObject overlap = GameObject.Find("Overlap");
        if (overlap == null)
        {
            overlap = new GameObject("Overlap", typeof(MeshFilter), typeof(MeshRenderer));
        }
        overlap.GetComponent<MeshFilter>().mesh = mesh;
        overlap.GetComponent<MeshRenderer>().material = overlapMaterial;
    }
}
