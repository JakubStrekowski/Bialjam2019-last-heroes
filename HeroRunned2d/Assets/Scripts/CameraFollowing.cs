using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    public float FollowSpeed = 4f;
    public Transform Target;
    public float yOffset = 7.3f;
    public float z = -10f;

    void Update()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = z;
        newPosition.y += yOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}
