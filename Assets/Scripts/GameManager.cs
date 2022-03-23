using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// your classic game manager
public class GameManager : MonoBehaviour
{
    List<ChemicalSpawner> spawners = new List<ChemicalSpawner>();

    void Start()
    {
        string[] guids;

        // search for a ScriptObject called ScriptObj
        guids = AssetDatabase.FindAssets("t:ChemicalSpawner");
        foreach (string guid in guids)
        {
            spawners.Add(AssetDatabase.LoadAssetAtPath<ChemicalSpawner>(AssetDatabase.GUIDToAssetPath(guid)));
        }
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
