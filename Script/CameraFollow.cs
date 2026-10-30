
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float smoothSpeed = 8f;
    public float lookHeight = 1.2f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.TransformPoint(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        Vector3 lookPosition =
            target.position + Vector3.up * lookHeight;

        transform.LookAt(lookPosition);
    }
}
