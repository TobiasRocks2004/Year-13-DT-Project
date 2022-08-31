using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSpawner : MonoBehaviour
{
    public string spawnerName;

    public GameObject prefab;
    [SerializeField]
    public ChemicalColor spawnColour;
    public Vector3 watcherPos;
    public Vector3 spawnPos;
    public int cooldown;

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
        if (!(CheckForChemical() || prefab == null) && timer <= 0)
        {
            GameObject chemical = Instantiate(prefab, spawnPos, Quaternion.identity);
            chemical.GetComponent<ChemicalItem>().color = spawnColour;

            timer = cooldown;
        }
        else
        {
            timer--;
        }
    }
}
