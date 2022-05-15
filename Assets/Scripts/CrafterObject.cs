using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Crafter scriptable object
public class CrafterObject : MonoBehaviour
{
    public string crafterName;

    public List<GameObject> slotPositions;

    public int runtime;

    public List<ListWrapper> reactants;
    public List<ListWrapper> products;

    public Dictionary<List<ChemicalItem>, List<ChemicalItem>> recipes;

    
    public void OnValidate()
    {
        recipes = new Dictionary<List<ChemicalItem>, List<ChemicalItem>>();
        for (int i = 0; i < reactants.Count; i++)
        {
            if (products[i] != null)
            {
                recipes.Add(reactants[i].list, products[i].list);
            }
            else
            {
                recipes.Add(reactants[i].list, null);
            }
        }
    }

    void Update()
    {
        
    }

    /*
    public bool CheckForValidRecipe()
    {
        HashSet<ChemicalItem> reactantsCheck = new HashSet<ChemicalItem>();

        foreach (GameObject slot in slotPositions) {
            Vector3 position = slot.transform.position;
            if (Physics.CheckSphere(position, 0.05f, LayerMask.GetMask("Flasks")))
            {

            }
        }
    }
    */
}

[System.Serializable]
public class ListWrapper
{
    public List<ChemicalItem> list;
}
