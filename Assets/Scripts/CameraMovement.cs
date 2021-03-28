using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera camera;
    public Transform player;
    public float cameraSpeed;
    public float leftBound;
    public float rightBound;

    public void MoveCamera(bool moveImmediately)
    {
        float viewWidth = camera.aspect * camera.orthographicSize * 2f;
        Vector3 cameraPosition = camera.transform.position;
        float minCameraX = leftBound + viewWidth / 2f;
        float maxCameraX = rightBound - viewWidth / 2f;
        cameraPosition.x = Mathf.Clamp(player.position.x, minCameraX, maxCameraX);
        if (moveImmediately)
        {
            camera.transform.position = cameraPosition;
        }
        else
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPosition, cameraSpeed * Time.deltaTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveCamera(true);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(false);
    }
}
