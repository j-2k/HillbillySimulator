using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        mouseX = 0;
        mouseY = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90, 90);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        //transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        //Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}
