using UnityEngine;

public class ParallaxTilemap : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect, 0f, 0f);
        lastCameraPosition = cameraTransform.position;
    }
}