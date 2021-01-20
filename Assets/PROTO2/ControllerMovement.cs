using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMovement : MonoBehaviour
{
    //==========VANILLA-MOVEMENT==========
    //movement
    [SerializeField] float speed;
    CharacterController controller;

    //gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocityG;
    
    //groundcheck
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.25f;      //rad of the sphere
    [SerializeField] LayerMask groundMask;              //control what obj the sphere will check for
    bool isGrounded = false;
    //==========VANILLA-MOVEMENT==========


    //==========CHAINSAW-MOVEMENT==========
    [SerializeField] GameObject chainsawObj;
    [SerializeField] float chainsawRevTime;
    [SerializeField] float chainsawTimerMultiplyer;
    [SerializeField] bool isReving = false;
    [SerializeField] bool chainsawSprint = false;
    [SerializeField] bool chainsawFatigue = false;
    [SerializeField] bool chainsawCrash = false;
    [SerializeField] float doubleEngravingSpeed;

    [SerializeField] MouseLookScript mouseRestriction;
    //==========CHAINSAW-MOVEMENT==========

    //==========UI==========
    public Slider chainsawMeterUI;
    //==========UI==========

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mouseRestriction = GetComponentInChildren<MouseLookScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(chainsawSprint == false)
        {
            //==========VANILLA-MOVEMENT==========
            //movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }

        //gravity (time sq)
        velocityG.y += gravity * Time.deltaTime;

        controller.Move(velocityG * Time.deltaTime);
        
        
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocityG.y < 0)
        {
            velocityG.y = -1f;
        }
        //==========VANILLA-MOVEMENT==========

        //==========CHAINSAW-MOVEMENT==========
        if (chainsawRevTime > 0)
        {
            isReving = true;
        }
        else
        {
            isReving = false;
            chainsawRevTime = 0;
            chainsawMeterUI.gameObject.SetActive(false);
        }
        if (isReving == true)
        {
            chainsawObj.transform.Rotate(5, 0, 0);
        }
        else
        {
            chainsawObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (chainsawFatigue == true)
        {
            //fatigue
        }
        if (chainsawCrash == true)
        {
            //crash
        }
        if (Input.GetMouseButton(1))
        {
            if (chainsawSprint == false)
            {
                chainsawRevTime += chainsawTimerMultiplyer * Time.deltaTime;
                chainsawMeterUI.gameObject.SetActive(true);
                if (chainsawRevTime >= 3)
                {
                    chainsawSprint = true;
                    chainsawRevTime = 3;
                }
            }

            if(chainsawSprint == true)
            {
                //speed = doubleEngravingSpeed;
                //make a speed vec here
                controller.Move(transform.forward * doubleEngravingSpeed * Time.deltaTime);
                mouseRestriction.mouseSprint = true;
                chainsawObj.transform.Rotate(30, 0, 0);
            }
        }
        else
        {
            chainsawRevTime -= chainsawTimerMultiplyer * Time.deltaTime;
            if (chainsawSprint == true)
            {
                chainsawRevTime = 0;
                chainsawSprint = false;
                mouseRestriction.mouseSprint = false;
                chainsawFatigue = true;
            }
        }
        //==========CHAINSAW-MOVEMENT==========
        //==========UI==========
        SetMeter();
        //==========UI==========
    }
    //==========UI==========
    public void SetMeter()
    {
        chainsawMeterUI.value = chainsawRevTime;
    }
    //==========UI==========
}
