using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;
    private const float MaxVerticalAngle = 80f; 
    private float _verticalRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 
        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
        
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -MaxVerticalAngle, MaxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
}
