using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : CrafterObject
{
    public override void ExecuteCraft()
    {
        ChemicalColor output;

        GetColors();

        if (colors.Count == 1)
        {
            if (colors[0].b != 3)
            {
                output = new ChemicalColor(colors[0].r, colors[0].g, colors[0].b + 1);
            }
            else
            {
                output = new ChemicalColor(colors[0].r - 1, colors[0].g - 1, colors[0].b);
            }
        }
        else
        {
            return;
        }

        SetSlot(0, output);
    }
}
