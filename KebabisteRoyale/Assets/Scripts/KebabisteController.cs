using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KebabisteIntent
{
    public Kebabiste.Action action;
    public Ingredient ingredient;
}

public class KebabisteController : Kebabiste
{
    
    
    public void OrderIngredient(Ingredient ingredient)
    {
        money -= GameController.ingredientPrices[ingredient];
        ingredientAmounts[ingredient] += 5;
    }
    public void PrepareIngredient(Ingredient ingredient)
    {
        ingredientAmounts[ingredient] -= 1;
        ingredientsReadyToUse[ingredient] += 1;
    }
}
