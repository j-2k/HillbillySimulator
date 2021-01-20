using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    Rigidbody billyRB;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float speedCheck;
    //Camera
    Camera cam;
    public Vector3 offset;
    //Mouse
    public float mouseSensitivity = 10f;
    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        billyRB = GetComponent<Rigidbody>();
        mouseX = 0;
        mouseY = 0;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        //billyRB.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity; //* Time.deltaTime;
        mouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity; //* Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }

    void FixedUpdate()
    {
        speedCheck = billyRB.velocity.magnitude;
        Debug.DrawRay(transform.position, billyRB.velocity, Color.blue);
        //
        float x = Input.GetAxis("Horizontal"); //* maxWalkSpeed;
        float y = Input.GetAxis("Vertical"); //* maxWalkSpeed;

        Vector3 movePos = (transform.right * x + transform.forward * y) * maxWalkSpeed;
        Vector3 newMovePos = new Vector3(movePos.x, billyRB.velocity.y, movePos.z);

        billyRB.velocity = newMovePos + new Vector3(0, billyRB.velocity.y, 0);

        if (billyRB.velocity.magnitude > maxWalkSpeed)
        {
            billyRB.velocity = Vector3.ClampMagnitude(billyRB.velocity, maxWalkSpeed);
        }

        cam.transform.position = billyRB.position + offset;
        
        //
        /*
        mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity;// * Time.deltaTime;
        mouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;// * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90, 90);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        */
    }
}
