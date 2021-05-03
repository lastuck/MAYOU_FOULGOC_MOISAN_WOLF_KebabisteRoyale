using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static KebabisteController kebabiste1;
    public static KebabisteController kebabiste2;

    [SerializeField] private bool keb1isAI;
    [SerializeField] private bool keb2isAI;
    
    [SerializeField] private Text SheepAmount; 
    [SerializeField] private Text ChickenAmount; 
    [SerializeField] private Text SteakAmount; 
    [SerializeField] private Text SaladAmount; 
    [SerializeField] private Text TomatoesAmount; 
    [SerializeField] private Text OnionsAmount; 
    [SerializeField] private Text SamuraiAmount; 
    [SerializeField] private Text WhiteAmount; 
    [SerializeField] private Text BBQAmount; 
    [SerializeField] private Text KetchupAmount; 
    [SerializeField] private Text MayoAmount; 
    
    private void Start()
    {
        InitPrices();
        kebabiste1 = new KebabisteController();
        kebabiste2 = new KebabisteController();
        
        
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

    public static void UpdateAmounts()
    {
        
    }
}
