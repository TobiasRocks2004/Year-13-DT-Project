using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chemical scriptable object
[CreateAssetMenu(fileName = "New Chemical Item", menuName = "Scriptables/Chemical")]
public class ChemicalItem : ScriptableObject
{
    public GameObject prefab;
}
