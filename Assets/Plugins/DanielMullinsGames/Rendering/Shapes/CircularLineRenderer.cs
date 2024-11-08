using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircularLineRenderer : ManagedBehaviour
{
    [SerializeField]
    private float value = 1;
    [SerializeField]
    private int segments = 360;
    [SerializeField]
    private int startDegree = 0;
    [SerializeField]
    private float width = 0.05f;
    [SerializeField]
    private Vector2 radii = Vector2.one;

    public bool drawOnStart = true;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(drawOnStart)
            DrawCircle();
    }

    [Button("Draw")]
    private void Draw()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawCircle();
    }

    public void DrawCircle(float value = -1, int startDegree = -1, float width = -1, Vector2 radii = new Vector2(), int segments = -1)
    {
        if (value == -1) value = this.value;
        else this.value = value;
        
        if (width == -1) width = this.width;
        else this.width = width;

        if (segments == -1) segments = this.segments;
        else this.segments = segments;

        if (radii == new Vector2()) radii = this.radii;
        else this.radii = radii;

        if (startDegree == -1) startDegree = this.startDegree;
        else this.startDegree = startDegree;

        lineRenderer.loop = value == 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        // add extra point to make startpoint and endpoint the same to close the circle
        int pointCount = (int)((segments + 1) * value); 
        var points = new Vector3[pointCount];
        lineRenderer.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments + startDegree);
            points[i] = new Vector3(Mathf.Sin(rad) * radii.x, Mathf.Cos(rad) * radii.y, 0);
        }

        lineRenderer.SetPositions(points);
    }
}
