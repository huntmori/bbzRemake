using UnityEngine;

using System.Collections;



public class WASDMovement : MonoBehaviour
{
    public float speed;

    Rigidbody rigidbody;
    Vector3 movement;
    float h, v;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }



    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        h = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);
    }

}