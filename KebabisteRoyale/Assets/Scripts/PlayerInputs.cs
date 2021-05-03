using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Button orderSheep;
    [SerializeField] private Button orderSteak;
    [SerializeField] private Button orderChicken;
    [SerializeField] private Button orderSalad;
    [SerializeField] private Button orderTomatoes;
    [SerializeField] private Button orderOnions;
    [SerializeField] private Button orderSamurai;
    [SerializeField] private Button orderWhite;
    [SerializeField] private Button orderBBQ;
    [SerializeField] private Button orderKetchup;
    [SerializeField] private Button orderMayo;

    [SerializeField] private Button prepareSheep;
    [SerializeField] private Button prepareSteak;
    [SerializeField] private Button prepareChicken;
    [SerializeField] private Button prepareSalad;
    [SerializeField] private Button prepareTomatoes;
    [SerializeField] private Button prepareOnions;
    [SerializeField] private Button prepareSamurai;
    [SerializeField] private Button prepareWhite;
    [SerializeField] private Button prepareBBQ;
    [SerializeField] private Button prepareKetchup;
    [SerializeField] private Button prepareMayo;

    public KebabisteIntent intent = null;
    private void Start()
    {
        orderSheep.onClick.AddListener(
            delegate { 
                intent = new KebabisteIntent
                    {
                        action = Kebabiste.Action.OrderIngredient,
                        ingredient = Ingredient.Sheep
                    };
            });
        orderSteak.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Steak); });
        orderChicken.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Chicken); });
        orderSalad.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Salad); });
        orderTomatoes.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Tomatoes); });
        orderOnions.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Onions); });
        orderSamurai.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Samurai); });
        orderWhite.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.White); });
        orderBBQ.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.BBQ); });
        orderKetchup.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Ketchup); });
        orderMayo.onClick.AddListener(delegate { GameController.kebabiste1.OrderIngredient(Ingredient.Mayo); });
        
        prepareSheep.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Sheep); });
        prepareSteak.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Steak); });
        prepareChicken.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Chicken); });
        prepareSalad.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Salad); });
        prepareTomatoes.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Tomatoes); });
        prepareOnions.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Onions); });
        prepareSamurai.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Samurai); });
        prepareWhite.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.White); });
        prepareBBQ.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.BBQ); });
        prepareKetchup.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Ketchup); });
        prepareMayo.onClick.AddListener(delegate { GameController.kebabiste1.PrepareIngredient(Ingredient.Mayo); });
    }

    
}
