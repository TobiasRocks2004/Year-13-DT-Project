using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public bool occupied = false;
    [HideInInspector]
    public GameObject occupation;

    public bool isEntry = true;
    public bool isExit = true;

    public GameObject template;

    public void ClearSlot()
    {
        occupation = null;
        occupied = false;
        if (transform.childCount != 0) {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void AssignSlot(GameObject occupation)
    {
        this.occupation = occupation;
        occupied = true;
    }

    public void RegenerateSlot()
    {
        GameObject chemical = Instantiate(occupation, transform.position, Quaternion.identity);
        chemical.transform.parent = transform;

        chemical.GetComponent<Rigidbody>().isKinematic = true;
        chemical.GetComponent<Collider>().enabled = false;
    }
}
