using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class StalkerCamera : MonoBehaviour
{
   //추적할 대상

    public Transform target;

    //카메라와의 거리

    public float dist = 4f;



    //카메라 회전 속도

    public float xSpeed = 220.0f;

    public float ySpeed = 100.0f;



    //카메라 초기 위치

    private float x = 0.0f;

    private float y = 0.0f;



    //y값 제한 (위 아래 제한)

    public float yMinLimit = -20f;

    public float yMaxLimit = 80f;



    //앵글의 최소,최대 제한

    float ClampAngle(float angle, float min, float max)

    {

        if (angle < -360)

            angle += 360;

        if (angle > 360)

            angle -= 360;

        return Mathf.Clamp(angle, min, max);

    }

    void Start()

    {

        Cursor.lockState = CursorLockMode.Locked; //커서 고정

        Vector3 angles = transform.eulerAngles;



        x = angles.y;

        y = angles.x;

    }



    void Update()

    {

        //카메라 회전속도 계산

        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;

        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;



        //앵글값 정하기(y값 제한)

        y = ClampAngle(y, yMinLimit, yMaxLimit);



        //카메라 위치 변화 계산

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);



        transform.rotation = rotation;

        target.rotation = Quaternion.Euler(0, x, 0);

        transform.position = position;

    }

}