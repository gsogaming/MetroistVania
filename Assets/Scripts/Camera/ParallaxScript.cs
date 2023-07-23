using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 1f;  // Controls the speed of the parallax effect

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;
        float deltaY = cameraTransform.position.y - lastCameraPosition.y;

        Vector3 deltaMovement = new Vector3(deltaX, deltaY, 0f);
        transform.position += deltaMovement * parallaxFactor;

        lastCameraPosition = cameraTransform.position;
    }
}
