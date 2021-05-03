using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KebabisteIntent
{
    public Kebabiste.Action action;
    public Ingredient ingredient;
}

public class KebabisteController
{
    private bool isAI;
    private Kebabiste kebabiste;
    
    public void OrderIngredient(Ingredient ingredient)
    {
        kebabiste.money -= GameController.ingredientPrices[ingredient];
        kebabiste.ingredientAmounts[ingredient] += 5;
    }
    public void PrepareIngredient(Ingredient ingredient)
    {
        kebabiste.ingredientAmounts[ingredient] -= 1;
        kebabiste.ingredientsReadyToUse[ingredient] += 1;
    }
}
