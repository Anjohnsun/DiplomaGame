using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 1;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target.position + offset, ref velocity, _smoothTime);
        }
    }
}
