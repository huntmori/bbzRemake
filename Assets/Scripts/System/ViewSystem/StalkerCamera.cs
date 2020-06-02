using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class StalkerCamera : MonoBehaviour
{
    public Transform Target;
    public float distance = 10f;
    public float height = 3f;
    public float dampTrace = 20f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
                                transform.position,
                                Target.position - (Target.forward * distance) + (Vector3.up * height),
                                Time.deltaTime * dampTrace);
        transform.LookAt(Target.position);
    }
}