using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class Movement3 : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Vector3 vectorAxis;
    [SerializeField] float speed;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float speedCheck;
    // Start is called before the first frame update
    void Start()
    {
        maxWalkSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity, Color.blue);

        speedCheck = rb.velocity.magnitude;

        //

        vectorAxis = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, Input.GetAxisRaw("Vertical") * speed) * Time.fixedDeltaTime;

        rb.velocity = vectorAxis;

        if (rb.velocity.magnitude > maxWalkSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxWalkSpeed);
        }
    }
} 
 
