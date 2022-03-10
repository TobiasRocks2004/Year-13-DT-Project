using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // reference to character controller
    public CharacterController controller;

    // variables to manage jumping
    public Transform groundCheck;
    public float groundDist = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;

    // physics variables
    public float speed = 12f;
    public float gravity = -9.8f;

    // store the object currently in the hand
    private GameObject heldObject;
    public Vector3 heldObjectPos = new Vector3(0.2f, 0.4f, 0.35f);

    public int pickupMaxDist = 2;

    Vector3 velocity;

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1)
        {
            move /= move.magnitude;
        }

        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                GetObjectAtMouse();
            }
            else
            {
                PlaceObjectAtMouse();
            }
        }
    }

    void GetObjectAtMouse()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 9 && Vector3.Distance(hit.collider.transform.position, transform.position) < pickupMaxDist) //layer 9: Flasks
            {
                heldObject = hit.collider.gameObject;
                heldObject.transform.parent = transform;

                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.GetComponent<BoxCollider>().enabled = false;
                heldObject.transform.localPosition = heldObjectPos;
            }
        }
    }

    void PlaceObjectAtMouse()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            heldObject.transform.parent = null;

            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<BoxCollider>().enabled = true;
            heldObject.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);

            heldObject = null;
        }
    }

}
