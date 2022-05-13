using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    Vector3 startTouch;

    [SerializeField]
    SpriteRenderer map;

    //mapBoundaries on X Axis: 
    float mapMinX, mapMaxX;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        mapMinX = map.transform.position.x - map.bounds.size.x / 2f;
        mapMaxX = map.transform.position.x + map.bounds.size.x / 2f;
        mainCamera.transform.position = MoveCameraWithinBoundaries(mainCamera.transform.position);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 distance = startTouch - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(distance.x, 0, 0);
            mainCamera.transform.position = MoveCameraWithinBoundaries(mainCamera.transform.position + targetPosition ); 
        }
    }

    private Vector3 MoveCameraWithinBoundaries(Vector3 targetPosition)
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float minX = mapMinX + cameraWidth;
        float maxX = mapMaxX - cameraWidth;

        float newXPosition = Mathf.Clamp(targetPosition.x, minX, maxX);

        return new Vector3(newXPosition,targetPosition.y,targetPosition.z);
   
    }
}
