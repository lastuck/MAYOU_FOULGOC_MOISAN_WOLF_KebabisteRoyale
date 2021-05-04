using System;
using System.Collections.Generic;

public class KebabisteIntent
{
    public Kebabiste.Action action;
    public Ingredient ingredient;
}

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
    public List<Ingredient> recipe;
    public int servedCount = 0;
    public List<Customer> customers;
    public int preparationTime = 1;
    public int orderingTime = 2;
    public int servingTime = 3;
    public int restTime = 3;
    public int corruptionTime = 20;
    public bool preparation;
    public bool serving;
    public bool corrupting;
    public bool fakeCustomerInQueue;
    

    public List<Ingredient> orderedIngredients;
    public bool unableToAct;
    public Kebabiste opponent;

    public Kebabiste()
    {
        ingredientAmounts = new Dictionary<Ingredient, int>();
        ingredientsReadyToUse = new Dictionary<Ingredient, int>();
        orderedIngredients = new List<Ingredient>();
        customers = new List<Customer>();
        foreach (Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
        {
            ingredientAmounts.Add(ingredient, 0);
            ingredientsReadyToUse.Add(ingredient, 0);
        }

        money = 1000;
        stress = 0;
    }

    public virtual KebabisteIntent GetIntent()
    {
        return null;
    }
}
