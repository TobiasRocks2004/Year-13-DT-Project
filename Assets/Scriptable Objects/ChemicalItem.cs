using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chemical Item", menuName = "Scriptables/Chemical")]
public class ChemicalItem : ScriptableObject
{
    public GameObject prefab;

    public CrafterObject madeIn;
    public ChemicalItem[] madeWith;

    public void OnValidate()
    {
        if (madeIn != null)
        {
            madeWith = new ChemicalItem[madeIn.slotCount];
        }
        else
        {
            madeWith = new ChemicalItem[0];
        }
    }
}
