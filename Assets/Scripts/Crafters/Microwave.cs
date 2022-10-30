using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Microwave : CrafterObject
{
    public override void ExecuteCraft()
    {
        ChemicalColor output;

        GetColors();

        if (colors.Count == 1)
        {
            output = new ChemicalColor(colors[0].r + 1, colors[0].g + 1, colors[0].b + 1);
        }
        else
        {
            return;
        }

        SetSlot(0, output);
    }
}
