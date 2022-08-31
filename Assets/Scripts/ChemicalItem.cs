using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalItem : MonoBehaviour
{
    public int id;

    public Material baseMat;

    [SerializeField]
    public ChemicalColor color;

    void OnValidate()
    {
        color.Clamp();
    }

    void Update()
    {
        color.Clamp();
        UpdateMaterialColor();
    }

    public void UpdateMaterialColor()
    {
        Color matColor = new Color((float)color.r / 3, (float)color.g / 3, (float)color.b /3, 1);

        Transform liquid = transform.GetChild(0);
        Material liquidMaterial = liquid.GetComponent<Renderer>().material;

        if (liquidMaterial.name != "liquid")
        {
            liquidMaterial = new Material(baseMat);
            liquidMaterial.name = "liquid";
        }
        liquid.GetComponent<Renderer>().material.color = matColor * 2;
    }
}