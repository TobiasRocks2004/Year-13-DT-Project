using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // reference to character controller
    public CharacterController controller;

    // variables to manage jumping
    [Space(10)]
    public Transform groundCheck;
    public float groundDist = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;

    // physics variables
    [Space(10)]
    public float speed = 8f;
    public float gravity = -9.8f;

    // store the object currently in the hand
    [Space(10)]
    private GameObject heldObject;
    public Vector3 heldObjectPos = new Vector3(0.2f, 0.4f, 0.35f);

    // maximum distance at which interaction is still possible
    [Space(10)]
    public float interactMaxDist = 2f;

    // internal variable to store velocity
    Vector3 velocity;

    // player control code courtesy of Brackeys
    void FixedUpdate()
    {
        // get input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // clamps speed to prevent moving faster on diagonals
        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1)
        {
            move /= move.magnitude;
        }

        // actually move
        controller.Move(move * speed * Time.deltaTime);

        // artificial gravity, just in case
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
        // detect a click
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = null;

            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                hitObject = hit.transform.gameObject;
            }

            if (hitObject != null && Vector3.Distance(hit.point, transform.position) < interactMaxDist)
            {
                if (hitObject.layer == LayerMask.NameToLayer("Flasks") && heldObject == null)
                {
                    // make it appear in the player's "hand"
                    heldObject = hitObject;
                    heldObject.transform.parent = transform;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = false;
                    heldObject.transform.localPosition = heldObjectPos;
                    heldObject.transform.localRotation = Quaternion.identity;
                }



                else if (hitObject.layer == LayerMask.NameToLayer("Microwave Door") && heldObject == null)
                {
                    if (hitObject.GetComponent<Transform>().rotation.y != 0)
                    {
                        hitObject.GetComponent<Animator>().SetTrigger("Close");
                        hitObject.GetComponentInParent<CrafterObject>().StartIfValidRecipe();
                    }
                    else
                    {
                        hitObject.GetComponent<Animator>().SetTrigger("Open");
                        hitObject.GetComponentInParent<CrafterObject>().StopTimer();
                    }
                }



                else if (hitObject.layer == LayerMask.NameToLayer("Slot"))
                {
                    Slot slot = hitObject.GetComponent<Slot>();
                    if (!slot.occupied && slot.isEntry && heldObject != null)
                    {
                        PlaceObjectInSlot(slot);
                    }
                    else if (slot.occupied && slot.isExit && heldObject == null)
                    {
                        GetObjectInSlot(slot);
                    }
                }



                else if (heldObject != null)
                {
                    PlaceObjectAtMouse();
                }
            }
        }
    }

    // function that places the held object at the position under the mouse
    void PlaceObjectAtMouse()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(hit.point, transform.position) < interactMaxDist)
            {
                // place it at the ray hit location
                heldObject.transform.parent = null;

                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Collider>().enabled = true;

                // offset the place position so it doesn't have any weird collisions
                Vector3 normal = hit.normal;
                heldObject.transform.position = new Vector3(hit.point.x + normal.x * 0.08f, hit.point.y + normal.y * 0.01f, hit.point.z + normal.z * 0.08f);

                heldObject = null;
            }
        }
    }

    void PlaceObjectInSlot(Slot slot)
    {
        if (Vector3.Distance(slot.transform.position, transform.position) < interactMaxDist)
        {
            if (heldObject != null)
            {
                heldObject.transform.parent = slot.transform;
                heldObject.transform.position = slot.transform.position;
                heldObject.transform.localRotation = Quaternion.identity;
                slot.SetSlot(heldObject);
                heldObject = null;
            }
        }
    }

    void GetObjectInSlot(Slot slot)
    {
        if (Vector3.Distance(slot.transform.position, transform.position) < interactMaxDist)
        {
            heldObject = slot.occupation;
            heldObject.transform.parent = transform;
            heldObject.transform.localPosition = heldObjectPos;
            heldObject.transform.localRotation = Quaternion.identity;

            slot.ClearSlot();
        }
    }
}
