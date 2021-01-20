using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookScript : MonoBehaviour
{
    //Mouse X = LEFT/RIGHT | Mouse Y = UP/DOWN

    public float mouseSensitivity = 100f;

    public float steeringSensitivity = 10f;

    public Transform playerBody;

    float xRot = 0f;

    public bool mouseSprint = false;

    [SerializeField] float mouseX;
    [SerializeField] float mouseY;
    [SerializeField] float chainsawSteeringXQE;
    [SerializeField] float timer = 0;
    //[SerializeField] float chainsawSteeringXM;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Camera.main.fieldOfView = 90;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Camera.main.fieldOfView = 120;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Camera.main.fieldOfView = 150;
        }

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        chainsawSteeringXQE = Input.GetAxisRaw("HorizontalQE") * steeringSensitivity * Time.deltaTime;
        //chainsawSteeringXM = Input.GetAxisRaw("Mouse X") * steeringSensitivity * Time.deltaTime;

        if (mouseSprint == false)
        {
            steeringSensitivity = 1000;
            timer = 0;
            xRot -= mouseY;
            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f); //UP/DOWN
            xRot = Mathf.Clamp(xRot, -90, 90);
            playerBody.Rotate(Vector3.up * mouseX); //LEFT/RIGHT | Moving Player body accoridng to up Vec aswell
            playerBody.Rotate(Vector3.up * chainsawSteeringXQE);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            playerBody.Rotate(Vector3.up * chainsawSteeringXQE); //LEFT/RIGHT | Moving Player body accoridng to up Vec aswell
            timer += Time.deltaTime;
            if (timer <= 1)
            {
                steeringSensitivity = 120;
            }
            if (timer > 1)
            {
                steeringSensitivity = 30;
            }
        }


        /*
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        */
    }
}
