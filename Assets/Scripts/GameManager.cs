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

    // tries to spawn a chemical every frame
    void Update()
    {
        foreach (ChemicalSpawner spawner in spawners)
        {
            spawner.SpawnChemical();
        }
    }
}
