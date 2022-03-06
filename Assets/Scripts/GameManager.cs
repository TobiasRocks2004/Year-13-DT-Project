using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    List<ChemicalSpawner> spawners = new List<ChemicalSpawner>();

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        foreach (ChemicalSpawner spawner in spawners)
        {
            spawner.SpawnChemical();
        }
    }
}
