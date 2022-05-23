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

    public Dictionary<List<GameObject>, List<GameObject>> recipes;

    List<GameObject> currentRecipe;
    bool running = false;
    float timer = 0;
    
    public void OnValidate()
    {
        recipes = new Dictionary<List<GameObject>, List<GameObject>>();
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
        if (running)
        {
            timer += Time.deltaTime;

            if (timer >= runtime)
            {
                running = false;
                timer = 0;

                ExecuteCraft();
            }
        }
    }

    public void StopTimer()
    {
        running = false;
        timer = 0;
    }

    void ExecuteCraft()
    {
        for (int i = 0; i < currentRecipe.Count; i++)
        {
            slotPositions[i].GetComponent<Slot>().occupation = null;
            Destroy(slotPositions[i].transform.GetChild(0).gameObject);
            slotPositions[i].GetComponent<Slot>().occupation = currentRecipe[i];
            slotPositions[i].GetComponent<Slot>().RegenSlot();
        }
    }

    public void StartIfValidRecipe()
    {
        HashSet<int> reactantsCheck = new HashSet<int>();

        foreach (GameObject slotObject in slotPositions) {
            Slot slot = slotObject.GetComponent<Slot>();
            if (slot.occupied)
            {
                reactantsCheck.Add(slot.occupation.GetComponent<ChemicalItem>().id);
            }
        }

        foreach (ListWrapper checkRecipe in reactants)
        {
            int correctFound = 0;
            foreach (GameObject reactant in checkRecipe.list)
            {
                if (reactantsCheck.Contains(reactant.GetComponent<ChemicalItem>().id))
                {
                    correctFound++;
                }

            }

            if (correctFound == checkRecipe.list.Count)
            {
                currentRecipe = recipes[checkRecipe.list];
                running = true;
                break;
            }
        }
    }
    
}

[System.Serializable]
public class ListWrapper
{
    public List<GameObject> list;
}
