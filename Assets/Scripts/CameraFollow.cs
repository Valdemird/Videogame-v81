using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player's transform
    public Vector3 offset = new Vector3(0f, 7f, -7f); // Offset for position
    public float smoothTime = 0.1f; // Smooth time for camera movement

    private Vector3 velocity = Vector3.zero;
    private void Start(){
        offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate target position with offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
