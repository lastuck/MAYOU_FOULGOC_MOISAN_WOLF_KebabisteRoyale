using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    public List<Ingredient> wantedRecipe;
    public int lifetime = 120;
    public bool isFake = false;

    private void Start()
    {
        StartCoroutine(UpdateLifetime());
        switch (Random.Range(0,2))
        {
            case 0:
                wantedRecipe.Add(Ingredient.Sheep);
                break;
            case 1:
                wantedRecipe.Add(Ingredient.Chicken);
                break;
            case 2:
                wantedRecipe.Add(Ingredient.Steak);
                break;
        }

        if (Random.Range(0, 100) < 80)
        {
            wantedRecipe.Add(Ingredient.Salad);
        }

        if (Random.Range(0, 100) < 80)
        {
            wantedRecipe.Add(Ingredient.Tomatoes);
        }

        if (Random.Range(0, 100) < 80)
        {
            wantedRecipe.Add(Ingredient.Onions);
        }
        switch (Random.Range(0,4))
        {
            case 0:
                wantedRecipe.Add(Ingredient.Samurai);
                break;
            case 1:
                wantedRecipe.Add(Ingredient.White);
                break;
            case 2:
                wantedRecipe.Add(Ingredient.BBQ);
                break;
            case 3:
                wantedRecipe.Add(Ingredient.Ketchup);
                break;
            case 4:
                wantedRecipe.Add(Ingredient.Mayo);
                break;
        }
    }

    private IEnumerator UpdateLifetime()
    {
        while (true)
        {
            --lifetime;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
