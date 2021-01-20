using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Vector3 vectorAxis;
    [SerializeField] float speed;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float speedCheck;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity,Color.blue);

        speedCheck = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += transform.forward * speed;// * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += transform.right * -speed;// * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += transform.forward * -speed;// * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += transform.right * speed;// * Time.fixedDeltaTime;
        }

        if (rb.velocity.magnitude >= maxWalkSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxWalkSpeed;//Vector3.ClampMagnitude(rb.velocity, maxWalkSpeed);
        }
    }
}
