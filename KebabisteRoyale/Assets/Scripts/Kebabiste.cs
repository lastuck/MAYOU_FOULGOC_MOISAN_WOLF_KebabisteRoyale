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
        CreateDish,
        FakeClient,
        Corrupt
    }
    

    private int money;
    private int stress;
    public const int MAX_STRESS = 100;
    private Dictionary<Ingredient, int> ingredientAmounts;
    
    
    public Kebabiste()
    {
        foreach (Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
        {
            ingredientAmounts.Add(ingredient, 0);
        }

        money = 1000;
        stress = 0;
    }

    public virtual Action GetIntent()
    {
        return Action.None;
    }
}
