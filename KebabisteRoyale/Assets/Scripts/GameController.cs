using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredient
{
    Sheep,
    Chicken,
    Steak,
    Salad,
    Onions,
    Tomatoes,
    Samurai,
    White,
    BBQ,
    Ketchup,
    Mayo
}

public class GameController : MonoBehaviour
{
    public static Dictionary<Ingredient, int> ingredientPrices;
    public static Kebabiste kebabiste1;
    public static Kebabiste kebabiste2;

    [SerializeField] private bool keb1isAI;
    [SerializeField] private bool keb2isAI;
    
    private void Start()
    {
        InitPrices();
        kebabiste1 = new Kebabiste();
        kebabiste2 = new Kebabiste();
        
        
    }

    private void InitPrices()
    {
        ingredientPrices.Add(Ingredient.Sheep, 100);
        ingredientPrices.Add(Ingredient.Chicken, 100);
        ingredientPrices.Add(Ingredient.Steak, 100);
        ingredientPrices.Add(Ingredient.Salad, 100);
        ingredientPrices.Add(Ingredient.Onions, 100);
        ingredientPrices.Add(Ingredient.Tomatoes, 100);
        ingredientPrices.Add(Ingredient.Samurai, 100);
        ingredientPrices.Add(Ingredient.White, 100);
        ingredientPrices.Add(Ingredient.BBQ, 100);
        ingredientPrices.Add(Ingredient.Ketchup, 100);
        ingredientPrices.Add(Ingredient.Mayo, 100);
    }
}
