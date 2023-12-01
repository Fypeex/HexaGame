using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private const float CameraSpeed = 0.003f;
    private const float CameraZoomSpeed = 0.01f;

    private float _mapMaxX;
    private float _mapMaxY;
    private float _mapMinX;
    private float _mapMinY;
    private const float MaxZoom = 8f;
    private const float MinZoom = 2f;
    private const float SwipeThreshHold = 0.2f;
    
    [field: SerializeField] private GameObject HexMap { get; set; }

    private Camera _camera;
    private Vector2 _fingerDown;
    private Vector2 _fingerUp;
    private EventSystem _eventSystem;


    private void Start()
    {
        _eventSystem = EventSystem.current;
        _camera = Camera.main;
        
        if (HexMap == null)
            HexMap = GameObject.Find("HexMap");

        foreach (Transform child in HexMap.transform)
        {
            float offset = _camera.orthographicSize - 1;
            var position = child.position;
            _mapMinX = Mathf.Min(_mapMinX, position.x + offset * 2);
            _mapMaxX = Mathf.Max(_mapMaxX, position.x - offset * 2);
            _mapMinY = Mathf.Min(_mapMinY, position.y + offset);
            _mapMaxY = Mathf.Max(_mapMaxY, position.y - offset);
        }
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {
                PrepareZoomTouch();
            }
        }
        else
        {
            PrepareZoomMouse();
        }
        
        if(Input.touches.Length == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
                _fingerUp = _fingerDown = t.position;

            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Ended)
            {
                _fingerDown = t.position;
                CheckSwipe();
            }
        }
    }

    private void PrepareZoomTouch()
    {
        // get current touch positions
        Touch tZero = Input.GetTouch(0);
        Touch tOne = Input.GetTouch(1);
        // get touch position from the previous frame
        Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
        Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

        float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
        float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

        // get offset value
        float deltaDistance = oldTouchDistance - currentTouchDistance;
        if (Mathf.Abs(deltaDistance) < SwipeThreshHold) return;
        Debug.Log($"DeltaDistance: {deltaDistance}");
        Zoom(deltaDistance, CameraZoomSpeed);
    }

    private void PrepareZoomMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(scroll) < SwipeThreshHold) return;
        Zoom(scroll, CameraZoomSpeed);
    }

    void CheckSwipe()
    {
        PointerEventData pointerData = new PointerEventData(_eventSystem)
        {
            position = _fingerDown
        };
            
        List<RaycastResult> results = new List<RaycastResult>();
        _eventSystem.RaycastAll(pointerData, results);

        if (results.Count > 0 && results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            return;
        }
        
        if (!(VerticalMove() > SwipeThreshHold) && !(HorizontalMove() > SwipeThreshHold))
        {
            Vector2 ray = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            
            if (hit.collider != null)
            {
                // Check if the ray hit a hex tile
                HexTile hexTile = hit.transform.GetComponent<HexTile>();
                if (hexTile != null)
                {
                    // Call the method to handle the click
                    hexTile.OnHexClicked();
                }
            }
            else
            {
                GameManager.Instance.ClearActiveTile();
            }
            
            return;
        }

        
        Swipe(_fingerUp, _fingerDown);
        _fingerUp = _fingerDown;
    }

    float VerticalMove()
    {
        return Mathf.Abs(_fingerDown.y - _fingerUp.y);
    }

    float HorizontalMove()
    {
        return Mathf.Abs(_fingerDown.x - _fingerUp.x);
    }

    //////////////////////////////////MOVEMENT FUNCTIONS/////////////////////////////
    void Swipe(Vector2 start, Vector2 end)
    {
        float yDistance = (end.y - start.y) * -1 * CameraSpeed;
        var position = _camera.transform.position;
        float newY = position.y + yDistance;
        newY = Mathf.Clamp(newY, _mapMinY, _mapMaxY);

        float xDistance = (end.x - start.x) * -1 * CameraSpeed;
        float newX = position.x + xDistance;
        newX = Mathf.Clamp(newX, _mapMinX, _mapMaxX);

        position = new Vector3(newX, newY, position.z);
        _camera.transform.position = position;
    }
    
    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        float newSize = Mathf.Clamp(_camera.orthographicSize + deltaMagnitudeDiff * speed, MinZoom, MaxZoom);
        
        _camera.orthographicSize = newSize;
    }
}