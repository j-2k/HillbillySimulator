using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Things needed to move camera around")]
    [Tooltip("The that will be followed and rotated")]
    [SerializeField] Transform target;
    [Tooltip("The mouse sensitivity, AKA rotation speed multiplier")]
    [SerializeField] float mouseSensitivity = 2;
    [Tooltip("Minimum vertical movement possible")]
    [SerializeField] float verticalClampMin = -15.0f;
    [Tooltip("Maximum vertical movement possible")]
    [SerializeField] float verticalClampMax = 60.0f;
    [Tooltip("Smoothening done while lerping the object rotation")]
    [SerializeField] float smoothCamRotation = 10.0f;

    float mouseX;                                            //The current horizontal input value for horizontal rotaion
    float mouseY;                                            //The current vertical input value for the vertical rotation

    [Header("Things needed to do wall clipping for the camera")]
    [Tooltip("Minimum distance the object will be from the target")]
    [SerializeField] float minDistance = 1.0f;
    [Tooltip("Maximum distance the object can be from the target")]
    [SerializeField] float maxDistance = 8.0f;
    [Tooltip("Smoothening done while lerping the object to desired position")]
    [SerializeField] float smoothCamMovement = 10.0f;
    [Tooltip("The layers that work for wall clipping")]
    [SerializeField] LayerMask WallClipLayerMask;

    float distance;                                          //The current distance the object will be from the target
    Vector3 camDirection;                 //vector3 that stores the local unit direction the object is from the camera
    Vector3 desiredCameraDir;         //The expected camera direction
    RaycastHit hit;                   //Object that stores details of the objects it hit during the linear cast

    void Awake()
    {

        Cursor.visible = false;                                     //Setting cursor to not be visible when playing the game
        Cursor.lockState = CursorLockMode.Locked;                   //Locking the cursor to the center of the screen so that it does not move out of the window
        //For wall clipping
        camDirection = transform.position - target.position;          //Getting the local unit direction vector
        distance = camDirection.magnitude;                          //Getting the local magnitude to the postion with is basically distance from parent to camera
    }

    void FixedUpdate()
    {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;               //Getting horizontal movement input of the mouse
            mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;               //Getting vertical movement input of the mouse
            mouseY = Mathf.Clamp(mouseY, verticalClampMin, verticalClampMax);    //Clamping the vertical value    //Setting object to always look at target

            // Clamping camera 
            desiredCameraDir = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.back;       //The direction the camera will be facing

            // Check if there is a wall or object between the camera and move the camera close to the target if so else set the camera to be at the normal distance from the target
            if (Physics.Raycast(target.transform.position, desiredCameraDir, out hit, maxDistance, WallClipLayerMask))
            {
                distance = Mathf.Clamp((hit.distance), minDistance, maxDistance);
            }
            else
            {
                distance = maxDistance;
            }
            transform.position = Vector3.Lerp(transform.position, desiredCameraDir * distance + target.position, Time.deltaTime * smoothCamMovement);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-desiredCameraDir), Time.deltaTime * smoothCamRotation);
        
    }
}
