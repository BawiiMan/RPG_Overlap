using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//메쉬 추가_06_10
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CircleRenderer : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 100;
    private LineRenderer lineRenderer;
    // 메쉬 추가_06_10
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;


    void Start()
    {
        // 메쉬 추가_06_10
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    public void FixedUpdate()
    {
        CreateCircle(segments, radius);
    }

    void CreateCircle(int steps, float radius)
    {
        lineRenderer.positionCount = steps + 1;
        
        for (int i = 0; i < steps; i++)
        {
            float progress = (float)i / steps;
            float currentRadian = progress * 2 * Mathf.PI;

            float scaleX = Mathf.Cos(currentRadian);
            float scaleY = Mathf.Sin(currentRadian);

            float x = scaleX * radius;
            float y = scaleY * radius;

            var pos = new Vector3(x, y, 0);

            pos += this.transform.position;

            lineRenderer.SetPosition(i, pos);
        }

        lineRenderer.SetPosition(steps, lineRenderer.GetPosition(0));

    }
}
