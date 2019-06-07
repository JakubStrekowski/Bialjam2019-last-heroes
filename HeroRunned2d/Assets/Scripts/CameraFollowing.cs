using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    public float FollowSpeed = 4f;
    public Transform Target;

    void Update()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}
