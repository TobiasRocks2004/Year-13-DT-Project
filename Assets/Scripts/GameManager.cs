using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// your classic game manager
public class GameManager : MonoBehaviour
{
    public List<ChemicalSpawner> spawners = new List<ChemicalSpawner>();
    public List<CrafterObject> crafters = new List<CrafterObject>();

    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Flasks"), LayerMask.NameToLayer("Slot"));
    }
}

[Serializable]
public struct ChemicalColor
{
    public int r;
    public int g;
    public int b;

    public void Clamp()
    {
        r = Mathf.Clamp(r, 0, 3);
        g = Mathf.Clamp(g, 0, 3);
        b = Mathf.Clamp(b, 0, 3);
    }

    public ChemicalColor(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;

        Clamp();
    }

    public void Print()
    {
        Debug.Log("r: " + r + ", g: " + g + ", b: " + b);
    }
}


public abstract class CrafterObject : MonoBehaviour
{
    public List<GameObject> slotPositions;

    private int startCount = 0;
    private int occupiedCount = 0;

    public bool autoRun;

    public int runtime;
    [HideInInspector]
    public bool running = false;
    float timer = 0;

    public GameObject template;

    public List<ChemicalColor> colours;

    void Start()
    {
        foreach (GameObject slot in slotPositions)
        {
            if (slot.GetComponent<Slot>().isEntry) startCount++;
        }
    }

    void Update()
    {
        if (autoRun) CheckIfValid();

        if (running)
        {
            timer += Time.deltaTime;

            if (timer >= runtime)
            {
                StopTimer();

                ExecuteCraft();
            }
        }
    }

    public void CheckIfValid()
    {
        foreach (GameObject slotObject in slotPositions)
        {
            Slot slot = slotObject.GetComponent<Slot>();

            occupiedCount = 0;
            if (slot.isEntry && slot.occupied) occupiedCount++;
        }

        if (occupiedCount == startCount)
        {
            if (!running) running = true;
        }
        else
        {
            if (running) running = false;
        }
    }

    public void StopTimer()
    {
        running = false;
        timer = 0;
    }

    public void GetColours()
    {
        colours = new List<ChemicalColor>();

        foreach(GameObject slotPosition in slotPositions)
        {
            if (slotPosition.GetComponent<Slot>().occupation != null)
            {
                colours.Add(slotPosition.GetComponent<Slot>().occupation.GetComponent<ChemicalItem>().color);
            }
        }
    }

    public void SetSlot(int index, ChemicalColor color)
    {
        Slot slot = slotPositions[index].GetComponent<Slot>();
        GameObject chemical = Instantiate(template, slot.transform.position, Quaternion.identity);

        chemical.GetComponent<ChemicalItem>().color = color;

        chemical.GetComponent<Rigidbody>().isKinematic = true;
        chemical.GetComponent<Collider>().enabled = false;

        slot.ClearSlot();

        chemical.transform.parent = slot.transform;

        slot.AssignSlot(chemical);
    }

    public abstract void ExecuteCraft();
}
