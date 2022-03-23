using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple script to control looking around
// camera control code courtesy of Brackeys
public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        playerBody = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
