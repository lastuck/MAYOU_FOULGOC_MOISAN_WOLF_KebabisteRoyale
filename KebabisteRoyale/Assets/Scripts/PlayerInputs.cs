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
    
    [SerializeField] private Button corrupt;
    [SerializeField] private Button fakeClient;
    [SerializeField] private Button serveOrder;
    [SerializeField] private Button takeBreak;
    

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
        orderSteak.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Steak
        }; });
        orderChicken.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Chicken
        };; });
        orderSalad.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Salad
        }; });
        orderTomatoes.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Tomatoes
        }; });
        orderOnions.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Onions
        }; });
        orderSamurai.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Samurai
        }; });
        orderWhite.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.White
        };});
        orderBBQ.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.BBQ
        }; });
        orderKetchup.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Ketchup
        }; });
        orderMayo.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.OrderIngredient,
            ingredient = Ingredient.Mayo
        }; });
        
        prepareSheep.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Sheep
        }; });
        prepareSteak.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Steak
        }; });
        prepareChicken.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Chicken
        }; });
        prepareSalad.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Salad
        }; });
        prepareTomatoes.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Tomatoes
        }; });
        prepareOnions.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Onions
        }; });
        prepareSamurai.onClick.AddListener(delegate {intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Samurai
        }; });
        prepareWhite.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.White
        }; });
        prepareBBQ.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.BBQ
        }; });
        prepareKetchup.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Ketchup
        }; });
        prepareMayo.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.PrepareIngredient,
            ingredient = Ingredient.Mayo
        }; });
        
        corrupt.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.Corrupt,
            ingredient = Ingredient.Mayo
        }; });
        fakeClient.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.FakeClient,
            ingredient = Ingredient.Mayo
        }; });
        serveOrder.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.CreateDish,
            ingredient = Ingredient.Mayo
        }; });
        takeBreak.onClick.AddListener(delegate { intent = new KebabisteIntent
        {
            action = Kebabiste.Action.TakeBreak,
            ingredient = Ingredient.Mayo
        }; });
    }

    
}
