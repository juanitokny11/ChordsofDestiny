﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Transform player;
    public CharacterController controller;
    public float maxHeight;
    public float maxPosX;
    public float maxPosZ;
    private Vector3 moveDirection = Vector3.zero;
    public float speed;
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;
    void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //moveDirection = transform.TransformDirection(moveDirection).normalized;
        moveDirection = moveDirection * speed;
        controller.Move(moveDirection * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            target.rotation = Quaternion.Euler(0, Input.mousePosition.x, 0);

            this.transform.position = new Vector3(transform.position.x, Input.mousePosition.y, transform.position.z);
        }
        if (this.transform.position.y<=maxHeight && this.transform.position.y>=0)
        { 
        transform.LookAt(target);
        }
        if (this.transform.position.y >= maxHeight)
        {
            this.transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        if (this.transform.position.y <= 0)
        {
            this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
