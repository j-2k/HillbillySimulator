using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody billyRB;
    GameObject billy;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float speedCheck;

    // Start is called before the first frame update
    void Start()
    {
        billyRB = GetComponent<Rigidbody>();
        billy = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, billyRB.velocity, Color.blue);
    }
    
    void FixedUpdate()
    {
        speedCheck = billyRB.velocity.magnitude;

        float x = Input.GetAxis("Horizontal"); //* maxWalkSpeed;
        float y = Input.GetAxis("Vertical"); //* maxWalkSpeed;

        Vector3 movePos = (transform.right * x + transform.forward * y) * maxWalkSpeed;
        Vector3 newMovePos = new Vector3(movePos.x, billyRB.velocity.y, movePos.z);

        billyRB.velocity = newMovePos;

        if (billyRB.velocity.magnitude > maxWalkSpeed)
        {
            billyRB.velocity = Vector3.ClampMagnitude(billyRB.velocity, maxWalkSpeed);
        }

        /*
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        billyRB.velocity = (transform.forward * vertical) + (transform.right * horizontal) * maxWalkSpeed * Time.fixedDeltaTime;


        billyRB.MovePosition
            */
        /*
        if (Input.GetKey(KeyCode.W))
        {
            billyRB.velocity += transform.forward * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            billyRB.velocity += transform.right * -3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            billyRB.velocity += transform.forward * -3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            billyRB.velocity += transform.right * 3 * Time.deltaTime;
        }
        if (billyRB.velocity.magnitude > maxWalkSpeed)
        {
            billyRB.velocity.magnitude =
        }
        */
    }
}
