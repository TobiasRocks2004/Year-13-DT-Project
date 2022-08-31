﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Microwave : CrafterObject
{
    public override void ExecuteCraft()
    {
        ChemicalColor output;

        GetColours();

        if (colours.Count == 1)
        {
            output = new ChemicalColor(colours[0].r + 1, colours[0].g + 1, colours[0].b + 1);
        }
        else
        {
            return;
        }

        SetSlot(0, output);
    }
}