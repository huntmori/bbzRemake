using UnityEngine;

using System.Collections;



public class WASDMovement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*5, 0));
    }

}