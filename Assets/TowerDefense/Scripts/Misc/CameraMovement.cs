using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float minZoom, maxZoom;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float WASDSpeed;

    [SerializeField] private float lerpAmount;

    private Vector3 dragOrigin;

    private static Camera cam => Helpers.Camera;


    public void SetRestrictions(float maxZoom, float minx, float miny, float maxx, float maxy)
    {
        this.maxZoom = maxZoom;
        minX = minx;
        maxX = maxx;
        minY = miny;
        maxY = maxx;
    }

    public void SetPosition(Vector3 position, float zoom)
    {
        MoveTo(position);
        SetZoom(zoom);
    }

    private void Update()
    {
        WASDMovement();
        PanCamera();
        InputZoom();
    }

    void WASDMovement()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        if (axis != 0)
            TranslateCamera(Vector2.right * WASDSpeed * Time.deltaTime * Mathf.Lerp(cam.orthographicSize, 0, .5f) * axis);
        axis = Input.GetAxisRaw("Vertical");
        if (axis != 0)
            TranslateCamera(Vector2.up * WASDSpeed * Time.deltaTime * Mathf.Lerp(cam.orthographicSize, 0, .5f) * axis);
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        if(Input.GetMouseButton(2))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            TranslateCamera(diff);
        }
    }

    private void InputZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
            ZoomTowards(- Input.mouseScrollDelta.y * zoomSpeed, Input.mousePosition);
    }
    public void ZoomTowards(float zoomAmount, Vector2 screenPos)
    {
        Vector2 from = cam.ScreenToWorldPoint(screenPos);
        ZoomBy(zoomAmount);
        Vector2 to = cam.ScreenToWorldPoint(screenPos);
        TranslateCamera(from - to);
    }
    public void ZoomBy(float amount)
        => SetZoom(cam.orthographicSize + amount);
    public void SetZoom(float zoom)
        => cam.orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);
        

    public void TranslateCamera(Vector3 moveBy)
    {
        float x = cam.transform.position.x + moveBy.x;
        float y = cam.transform.position.y + moveBy.y;

        MoveTo(x, y);
    }

    public void MoveTo(Vector3 destination)
        => MoveTo(destination.x, destination.y);
    public void MoveTo(float x, float y)
    {
        if      (x > maxX) x = maxX;
        else if (x < minX) x = minX;
        if      (y > maxY) y = maxY;
        else if (y < minY) y = minY;

        cam.transform.position = new Vector3(x, y, cam.transform.position.z);
    }
}
