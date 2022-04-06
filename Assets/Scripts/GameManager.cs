using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// your classic game manager
public class GameManager : MonoBehaviour
{
    List<ChemicalSpawner> spawners = new List<ChemicalSpawner>();
    // TODO write code to get the recipes and crafting done
    Dictionary<Colour, GameObject> correspondance = new Dictionary<Colour, GameObject>();
    Dictionary<Colour, Colour> recipes = new Dictionary<Colour, Colour>();

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

enum Colour
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
