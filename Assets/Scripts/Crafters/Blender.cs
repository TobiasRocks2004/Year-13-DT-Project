using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : CrafterObject
{
    public override void ExecuteCraft()
    {
        ChemicalColor output;

        GetColors();

        if (colors.Count == 2)
        {
            output = new ChemicalColor((colors[0].r + colors[1].r) / 2, (colors[0].g + colors[1].g) / 2, (colors[0].b + colors[1].b) / 2);
        }
        else
        {
            return;
        }

        slotPositions[1].GetComponent<Slot>().ClearSlot();
        SetSlot(0, output);
    }
}
