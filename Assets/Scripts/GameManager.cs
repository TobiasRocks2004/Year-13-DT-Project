using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// your classic game manager
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<ChemicalSpawner> spawners = new List<ChemicalSpawner>();
    [HideInInspector]
    public List<CrafterObject> crafters = new List<CrafterObject>();
    // TODO write code to get the recipes and crafting done
    public Dictionary<Colour, GameObject> correspondance = new Dictionary<Colour, GameObject>();

    void Start()
    {
        string[] guids;

        // search for a ScriptObject called ScriptObj
        guids = AssetDatabase.FindAssets("t:ChemicalSpawner");
        foreach (string guid in guids)
        {
            spawners.Add(AssetDatabase.LoadAssetAtPath<ChemicalSpawner>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        guids = AssetDatabase.FindAssets("t:CrafterObject");
        foreach (string guid in guids)
        {
            crafters.Add(AssetDatabase.LoadAssetAtPath<CrafterObject>(AssetDatabase.GUIDToAssetPath(guid)));
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

public enum Colour
{
    White,
    Light_Grey,
    Grey,
    Black,
    Red,
    Orange,
    Yellow,
    Green,
    Dark_Green,
    Cyan,
    Light_Blue,
    Blue,
    Purple,
    Magenta,
    Pink,
    Brown
}
