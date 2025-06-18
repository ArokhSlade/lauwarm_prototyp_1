#nullable disable

using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] Vector3 offsetFromObject = new(0, 10, -10);
    [SerializeField] float smoothTime;

    Vector3 currentVelocity;

    void LateUpdate()
    {
        if (targetObject == null) return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetObject.transform.position + offsetFromObject,
            ref currentVelocity,
            smoothTime
        );

        transform.LookAt(targetObject.transform);
    }
}
