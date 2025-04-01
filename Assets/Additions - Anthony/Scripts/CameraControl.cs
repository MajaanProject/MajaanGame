using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 camOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 camCurVelocity = Vector3.zero;

    private void Awake() 
    {
        camOffset = transform.position - target.position;
    }

    private void LateUpdate() 
    {
        Vector3 targetPosition = target.position + camOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref camCurVelocity, smoothTime);
    }

}
