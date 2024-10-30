using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Generic useful methods
public static class Helpers
{
    //Caches the main camera (because calling Camera.main
    //uses the FindObjectOfType function, which is very expensive)
    private static Camera _camera;
    public static Camera Camera {
        get {
            if (_camera == null) 
                _camera = Camera.main;
            return _camera;
        }
    }

    //Caches WaitForSeconds for coroutines
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait))
            return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    //returns true if the cursor is over some UI
    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _raycastResults;
    public static bool IsOverUI { 
        get { 
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _raycastResults);
            return _raycastResults.Count > 0;
        } 
    }
    

    public static Vector2 GetWorldPositionOfCanvasElement(this RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }

    //destroys all children of a GameObject
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform c in t) Object.Destroy(c.gameObject);
    }
}
