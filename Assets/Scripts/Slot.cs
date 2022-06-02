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

    public void ClearSlot()
    {
        occupation = null;
        occupied = false;
    }

    public void RegenSlot()
    {
        GameObject newFlask = Instantiate(occupation);
        newFlask.transform.parent = transform;
        newFlask.transform.position = transform.position;
        newFlask.transform.localRotation = Quaternion.identity;
        newFlask.GetComponent<Rigidbody>().isKinematic = true;
        newFlask.GetComponent<Collider>().enabled = false;
        SetSlot(newFlask);
    }

    public void SetSlot(GameObject prefab)
    {
        occupation = prefab;
        occupied = true;
    }
}
