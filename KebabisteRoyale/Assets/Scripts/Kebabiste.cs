using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebabiste
{
    public enum Action
    {
        None,
        OrderIngredient,
        PrepareIngredient,
        CreateDish,
        FakeClient,
        Corrupt,
        TakeBreak
    }
    
    public int money;
    public int stress;
    public const int MAX_STRESS = 100;
    public Dictionary<Ingredient, int> ingredientAmounts;
    public Dictionary<Ingredient, int> ingredientsReadyToUse;

    public Kebabiste()
    {
        foreach (Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
        {
            ingredientAmounts.Add(ingredient, 0);
        }

        money = 1000;
        stress = 0;
    }

    public virtual KebabisteIntent GetIntent()
    {
        return null;
    }
    
}
