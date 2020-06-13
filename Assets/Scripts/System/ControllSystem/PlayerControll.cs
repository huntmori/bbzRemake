using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Transform _transform;
    private float _horizontal = 0.0f;
    private float _vertical = 0.0f;

    public float move_speed = 5.0f;
    public float rotation_speed = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Vector3 move_direct = (Vector3.forward * _vertical) + (Vector3.right * _horizontal);
        
        _transform.Translate(move_direct.normalized * Time.deltaTime * move_speed, Space.Self);
        _transform.Rotate(Vector3.up * Time.deltaTime * rotation_speed * Input.GetAxis("Mouse X"));
    }
}
