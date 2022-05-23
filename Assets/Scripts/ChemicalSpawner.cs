﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSpawner : MonoBehaviour
{
    public string spawnerName;

    public GameObject chemical;
    public Vector3 watcherPos;
    public Vector3 spawnPos;
    public int cooldown = 600;

    private int timer = 0;

    public void Update()
    {
        SpawnChemical();
    }

    // checks whether a chemical is in the watching zone
    public bool CheckForChemical()
    {
        return Physics.CheckSphere(watcherPos, 0.2f, LayerMask.GetMask("Flasks"));
    }

    // spawns a chemical every [COOLDOWN] frames
    public void SpawnChemical()
    {
        if (!(CheckForChemical() || chemical == null) && timer <= 0)
        {
            Instantiate(chemical, spawnPos, Quaternion.identity);
            timer = cooldown;
        }
        else
        {
            timer--;
        }
    }
}