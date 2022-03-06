using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Station", menuName = "Scriptables/Crafting Station")]
public class CrafterObject : ScriptableObject
{
    public GameObject prefab;

    public int slotCount;
    public List<GameObject> slotPositions;

    public int runtime;

    public void OnValidate()
    {
        if (prefab != null)
        {
            for (int i = 0; i >= prefab.transform.childCount; i++)
            {
                GameObject child = prefab.transform.GetChild(i).gameObject;
                if (child.CompareTag("Slot Position") && slotPositions.Count < slotCount)
                {
                    slotPositions.Add(child);
                }
            }
        }
    }
}
