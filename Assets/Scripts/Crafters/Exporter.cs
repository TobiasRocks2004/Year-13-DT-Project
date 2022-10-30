using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Exporter : CrafterObject
{
    public GameObject display;
    public Material baseMat;
    public GameManager gameManager;

    public TextMeshProUGUI rVal;
    public TextMeshProUGUI gVal;
    public TextMeshProUGUI bVal;


    private ChemicalColor targetColor;

    public void Awake()
    {
        targetColor = ChemicalColor.GenerateRandom();

        UpdateDisplay();
    }

    public override void ExecuteCraft()
    {
        GetColors();

        if (slotPositions[0].GetComponent<Slot>().occupied)
        {

            if (colors[0].r == targetColor.r &&
                colors[0].g == targetColor.g &&
                colors[0].b == targetColor.b)
            {
                slotPositions[0].GetComponent<Slot>().ClearSlot();

                gameManager.score++;

                targetColor = ChemicalColor.GenerateRandom();

                UpdateDisplay();
            }
        }
    }

    public void UpdateDisplay()
    {
        Color matColor = new Color((float)targetColor.r / 3, (float)targetColor.g / 3, (float)targetColor.b / 3, 1);

        Material mat = new Material(baseMat);
        mat.color = matColor * 2;
        display.GetComponent<Renderer>().material = mat;

        rVal.text = targetColor.r.ToString();
        gVal.text = targetColor.g.ToString();
        bVal.text = targetColor.b.ToString();
    }
}
