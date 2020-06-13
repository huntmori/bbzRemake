using UnityEngine;

using System.Collections;
using System.Windows.Input;

public class WASDMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jump_power = 5f;
    public float rotation_speed = 3f;

    public float player_x_rotation_value;
    public float player_y_rotation_value;



    Rigidbody rigidbody;
    Vector3 movement;

    float horizontal_move;
    float vertical_move;
    
    bool is_jumping;

    public Camera cam;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        horizontal_move = Input.GetAxisRaw("Horizontal");
        vertical_move = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            is_jumping = true;
        }
    }
    private void FixedUpdate()
    {
        Run();
        Jump();
        Rotation();
    }

    void Jump()
    {
        if (!is_jumping)
            return;

        rigidbody.AddForce(Vector3.up * jump_power,
                            ForceMode.Impulse);

        is_jumping = false;
    }
    void Run()
    {
        //movement.Set(horizontal_move, 0, vertical_move);
        //movement = movement.normalized * speed * Time.deltaTime;

        //rigidbody.MovePosition(transform.position + movement);
        Vector3 moveDirection = new Vector3(horizontal_move, 0, vertical_move);
        this.rigidbody.AddForce(moveDirection * speed);
    }

    void Rotation()
    {
        float rotation_x = Input.GetAxis("Mouse Y") * rotation_speed;
        float rotation_y = Input.GetAxis("Mouse X") * rotation_speed;

        this.transform.localRotation *= Quaternion.Euler(0, rotation_y, 0);
        cam.transform.localRotation *= Quaternion.Euler(-rotation_x, 0, 0);
    }
}