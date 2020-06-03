using UnityEngine;

using System.Collections;



public class WASDMovement : MonoBehaviour
{
    public float move_speed;
    public float rotation_speed;

    public Camera fpsCam;

    private void Start()
    {
        
    }

    private void Update()
    {
        MoveControll();
        RotationControll();
    }

    void MoveControll()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        }
    }

    void RotationControll()
    {
        float rotation_x = Input.GetAxis("Mouse Y") * rotation_speed;
        float rotation_y = Input.GetAxis("Mouse X") * rotation_speed;

        this.transform.localRotation *= Quaternion.Euler(0, rotation_y, 0);
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotation_x, 0, 0);
    }
}