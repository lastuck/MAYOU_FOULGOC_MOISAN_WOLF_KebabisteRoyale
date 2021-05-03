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


    protected int money;
    protected int stress;
    public const int MAX_STRESS = 100;
    protected Dictionary<Ingredient, int> ingredientAmounts;
    protected Dictionary<Ingredient, int> ingredientsReadyToUse;
    protected bool isAI;
    
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
