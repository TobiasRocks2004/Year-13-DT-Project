using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chemical Spawner", menuName = "Scriptables/Chemical Spawner")]
public class ChemicalSpawner : ScriptableObject
{
    public GameObject chemical;
    public Vector3 watcherPos;
    public Vector3 spawnPos;

    private int cooldown = 0;

    public bool CheckForChemical()
    {
        return Physics.CheckSphere(watcherPos, 0.2f, LayerMask.GetMask("Flasks"));
    }

    public void SpawnChemical()
    {
        if (!(CheckForChemical() || chemical == null) && cooldown <= 0)
        {
            Instantiate(chemical, spawnPos, Quaternion.identity);
            cooldown = 300;
        }
        else
        {
            cooldown--;
        }
    }
}
