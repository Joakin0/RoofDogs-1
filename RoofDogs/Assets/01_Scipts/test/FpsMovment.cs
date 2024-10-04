using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovment : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5;
    bool grounded;
    public Transform transform;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float y = Input.GetAxisRaw("Vertical") * speed;

        //mov
        Vector3 movPos = transform.right * x + transform.forward * y;
        Vector3 newMovPos = new Vector3(movPos.x, rb.velocity.y, movPos.z);
        rb.velocity = newMovPos;

        //jummp
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            grounded = false;
    }
}
