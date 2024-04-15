using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    private bool isShaking = false;
    private readonly float shakeMagnitude = 2;
    private readonly float shakeDuration = 0.2F;
    private float shakeEndTime = 0f;

    private void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = CalculateTargetPositionWithOffset();

        if (isShaking && Time.time < shakeEndTime)
        {
            targetPosition = ApplyShakeEffect(targetPosition);
        }

        Vector3 newPostition = SmoothlyMoveCamera(targetPosition);
        transform.position = newPostition;
    }

    private Vector3 CalculateTargetPositionWithOffset()
    {
        return target.position + offset;
    }

    private Vector3 ApplyShakeEffect(Vector3 targetPosition)
    {
        float x = Mathf.PerlinNoise(Time.time * shakeMagnitude, 0) * 2 - 1;
        float y = Mathf.PerlinNoise(0, Time.time * shakeMagnitude) * 2 - 1;
        return targetPosition + new Vector3(x, y, 0);
    }

    private Vector3 SmoothlyMoveCamera(Vector3 targetPosition)
    {
        return  Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void StartShake()
    {
        if (target == null)
            return;

        isShaking = true;
        shakeEndTime = Time.time + shakeDuration;
    }

    public void StopShake()
    {
        isShaking = false;
    }
}