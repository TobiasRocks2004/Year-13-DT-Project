using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : CrafterObject
{
    public override void ExecuteCraft()
    {
        ChemicalColor output;

        GetColours();

        if (colours.Count == 1)
        {
            if (colours[0].r != 3)
            {
                output = new ChemicalColor(colours[0].r + 1, colours[0].g, colours[0].b);
            }
            else
            {
                output = new ChemicalColor(colours[0].r, colours[0].g - 1, colours[0].b - 1);
            }
        }
        else
        {
            return;
        }

        SetSlot(0, output);
    }
}
