using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class StalkerCamera : MonoBehaviour
{
    public Transform target_transform;
    public float distance = 7.0f;
    public float height = 2.0f;
    public float damp_trace = 20.0f;

    private Transform _transform;

    public void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _transform.position = Vector3.Lerp( _transform.position,
                                            target_transform.position - (target_transform.forward * distance),
                                            Time.deltaTime * damp_trace);
        _transform.LookAt(target_transform.position);
    }
}