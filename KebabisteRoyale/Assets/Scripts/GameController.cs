using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public static PlayerKebabiste kebabiste1;
    public static PlayerKebabiste kebabiste2;

    [SerializeField] private bool visualisAI;
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
    
    [SerializeField] private Text SheepAmountReady; 
    [SerializeField] private Text ChickenAmountReady; 
    [SerializeField] private Text SteakAmountReady; 
    [SerializeField] private Text SaladAmountReady; 
    [SerializeField] private Text TomatoesAmountReady; 
    [SerializeField] private Text OnionsAmountReady; 
    [SerializeField] private Text SamuraiAmountReady; 
    [SerializeField] private Text WhiteAmountReady; 
    [SerializeField] private Text BBQAmountReady; 
    [SerializeField] private Text KetchupAmountReady; 
    [SerializeField] private Text MayoAmountReady;
    
    [SerializeField] private Slider stressBar; 
    [SerializeField] private Text money; 
    
    [SerializeField] private Text EnemySheepAmount; 
    [SerializeField] private Text EnemyChickenAmount; 
    [SerializeField] private Text EnemySteakAmount; 
    [SerializeField] private Text EnemySaladAmount; 
    [SerializeField] private Text EnemyTomatoesAmount; 
    [SerializeField] private Text EnemyOnionsAmount; 
    [SerializeField] private Text EnemySamuraiAmount; 
    [SerializeField] private Text EnemyWhiteAmount; 
    [SerializeField] private Text EnemyBBQAmount; 
    [SerializeField] private Text EnemyKetchupAmount; 
    [SerializeField] private Text EnemyMayoAmount; 
    
    [SerializeField] private Slider EnemyStressBar; 
    [SerializeField] private Text EnemyMoney;

    [SerializeField] private GameObject customer;
    [SerializeField] private Transform[] customerPlaces;
    [SerializeField] private PlayerInputs playerInputs;
    
    private bool gameRunning = true;

    
    
    private void Start()
    {
        InitPrices();
        kebabiste1 = new PlayerKebabiste();
        kebabiste1.playerInputs = playerInputs;
        //kebabiste1.isAI = visualisAI;
        kebabiste2 = new PlayerKebabiste();
        kebabiste2.playerInputs = playerInputs;

        kebabiste1.opponent = kebabiste2;
        kebabiste2.opponent = kebabiste1;

        StartCoroutine(IncreaseStress());
    }

    private void InitPrices()
    {
        ingredientPrices = new Dictionary<Ingredient, int>();
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

    public void UpdateAmounts()
    {
        SheepAmount.text = kebabiste1.ingredientAmounts[Ingredient.Sheep].ToString();
        SteakAmount.text = kebabiste1.ingredientAmounts[Ingredient.Steak].ToString();
        ChickenAmount.text = kebabiste1.ingredientAmounts[Ingredient.Chicken].ToString();
        SaladAmount.text = kebabiste1.ingredientAmounts[Ingredient.Salad].ToString();
        TomatoesAmount.text = kebabiste1.ingredientAmounts[Ingredient.Tomatoes].ToString();
        OnionsAmount.text = kebabiste1.ingredientAmounts[Ingredient.Onions].ToString();
        SamuraiAmount.text = kebabiste1.ingredientAmounts[Ingredient.Samurai].ToString();
        WhiteAmount.text = kebabiste1.ingredientAmounts[Ingredient.White].ToString();
        BBQAmount.text = kebabiste1.ingredientAmounts[Ingredient.BBQ].ToString();
        KetchupAmount.text = kebabiste1.ingredientAmounts[Ingredient.Ketchup].ToString();
        MayoAmount.text = kebabiste1.ingredientAmounts[Ingredient.Mayo].ToString();
        
        SheepAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Sheep].ToString();
        SteakAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Steak].ToString();
        ChickenAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Chicken].ToString();
        SaladAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Salad].ToString();
        TomatoesAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Tomatoes].ToString();
        OnionsAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Onions].ToString();
        SamuraiAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Samurai].ToString();
        WhiteAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.White].ToString();
        BBQAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.BBQ].ToString();
        KetchupAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Ketchup].ToString();
        MayoAmountReady.text = kebabiste1.ingredientsReadyToUse[Ingredient.Mayo].ToString();

        stressBar.value = (float)kebabiste1.stress/100;
        money.text = kebabiste1.money.ToString();
        
        EnemySheepAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Sheep]+kebabiste2.ingredientsReadyToUse[Ingredient.Sheep]).ToString();
        EnemySteakAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Steak]+kebabiste2.ingredientsReadyToUse[Ingredient.Steak]).ToString();
        EnemyChickenAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Chicken]+kebabiste2.ingredientsReadyToUse[Ingredient.Chicken]).ToString();
        EnemySaladAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Salad]+kebabiste2.ingredientsReadyToUse[Ingredient.Salad]).ToString();
        EnemyTomatoesAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Tomatoes]+kebabiste2.ingredientsReadyToUse[Ingredient.Tomatoes]).ToString();
        EnemyOnionsAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Onions]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        EnemySamuraiAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Samurai]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        EnemyWhiteAmount.text = (kebabiste2.ingredientAmounts[Ingredient.White]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        EnemyBBQAmount.text = (kebabiste2.ingredientAmounts[Ingredient.BBQ]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        EnemyKetchupAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Ketchup]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        EnemyMayoAmount.text = (kebabiste2.ingredientAmounts[Ingredient.Mayo]+kebabiste2.ingredientsReadyToUse[Ingredient.Onions]).ToString();
        
        EnemyStressBar.value = (float)kebabiste2.stress/100;
        //EnemyMoney.text = kebabiste2.money.ToString();
    }

    private void SpawnCustomer(bool visual)
    {
        if (visual)
        {
            kebabiste1.customers.Add(Instantiate(customer, customerPlaces[kebabiste1.customers.Count].position, Quaternion.identity)
                .GetComponent<Customer>());
            kebabiste1.customers.Last().isFake = kebabiste1.fakeCustomerInQueue;
            kebabiste1.fakeCustomerInQueue = false;
        }
        else
        {
            kebabiste2.customers.Add(Instantiate(customer, new Vector3(100,100,100), Quaternion.identity)
                .GetComponent<Customer>());
            kebabiste2.customers.Last().isFake = kebabiste1.fakeCustomerInQueue;
            kebabiste2.fakeCustomerInQueue = false;
        }
    }

    private void UpdateCustomers(ref List<Customer> customersList, bool visual)
    {
        List<Customer> customerToRemove = new List<Customer>();
        int place = 0;
        foreach (Customer customer in customersList)
        {
            if (customer.lifetime < 1)
            {
                customer.gameObject.SetActive(false);
                customerToRemove.Add(customer);
                continue;
            }

            if (visual)
            {
                customer.gameObject.transform.position = customerPlaces[place].position;
            }

            place++;
        }
        foreach (Customer customer in customerToRemove)
        {
            customersList.Remove(customer);
        }

        if (visual && customersList.Count > 0)
        {
            if (kebabiste1.recipe != customersList.First().wantedRecipe)
            {
                kebabiste1.recipe = customersList.First().wantedRecipe;
                string recipeStr = "";
                foreach (Ingredient ingredient in kebabiste1.recipe)
                {
                    recipeStr+=ingredient+" ";
                }
                Debug.Log(recipeStr);
            }
        }
        else if (customersList.Count > 0)
        {
            if (kebabiste2.recipe != customersList.First().wantedRecipe)
            {
                kebabiste2.recipe = customersList.First().wantedRecipe;
            }
        }

    }

    private void Update()
    {
        if (gameRunning)
        {
            UpdateAmounts();
            if (kebabiste1.customers.Count > 0)
            {
                UpdateCustomers(ref kebabiste1.customers, true);
            }

            if (kebabiste2.customers.Count > 0)
            {
                UpdateCustomers(ref kebabiste2.customers, false);
            }

            if (kebabiste1.customers.Count < 3)
            {
                SpawnCustomer(true);
            }
            if (kebabiste2.customers.Count < 3)
            {
                SpawnCustomer(false);
            }

            if (kebabiste1.stress >= 100)
            {
                StartCoroutine(Stun(kebabiste1));
            }
            if (kebabiste2.stress >= 100)
            {
                StartCoroutine(Stun(kebabiste2));
            }

            PlayActions(kebabiste1, true);
            PlayActions(kebabiste2, false);
            
            if (kebabiste1.servedCount > 10)
            {
                Debug.Log("kebabiste1 a gagné");
                gameRunning = false;
            }

            if (kebabiste2.servedCount > 10)
            {
                Debug.Log("kebabiste2 a gagné");
                gameRunning = false;
            }
        }
    }

    public IEnumerator IncreaseStress()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            kebabiste1.stress += 2;
            kebabiste2.stress += 2;
        }
    }

    public void PlayActions(Kebabiste kebabiste, bool visual)
    {
        if (!kebabiste.unableToAct)
        {
            KebabisteIntent kebabisteIntent = kebabiste.GetIntent();
            if (kebabisteIntent != null)
            {
                switch (kebabisteIntent.action)
                {
                    case Kebabiste.Action.OrderIngredient:
                        if (!kebabiste.orderedIngredients.Contains(kebabisteIntent.ingredient))
                        {
                            StartCoroutine(OrderIngredient(kebabisteIntent.ingredient, kebabiste));
                        }

                        break;
                    case Kebabiste.Action.PrepareIngredient:
                        if (kebabiste.preparation == false)
                        {
                            StartCoroutine(PrepareIngredient(kebabisteIntent.ingredient, kebabiste));
                        }

                        break;
                    case Kebabiste.Action.Corrupt:
                        if (kebabiste.corrupting == false)
                        {
                            StartCoroutine(Corrupt(kebabiste));
                        }

                        break;
                    case Kebabiste.Action.FakeClient:
                        FakeCustomer(kebabiste);
                        break;
                    case Kebabiste.Action.CreateDish:
                        if (kebabiste.serving == false)
                        {
                            StartCoroutine(CreateDish(kebabiste));
                        }

                        break;
                    case Kebabiste.Action.TakeBreak:
                        StartCoroutine(TakeBreak(kebabiste));
                        break;
                    case Kebabiste.Action.None:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public IEnumerator Stun(Kebabiste kebabiste)
    {
        int cpt = 0;
        kebabiste.unableToAct = true;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
            if (cpt >= 6)
            {
                break;
            }
        }
        kebabiste.unableToAct = false;
        kebabiste.stress = 0;
    }
    
    public IEnumerator OrderIngredient(Ingredient ingredient, Kebabiste kebabiste)
    {
        int cpt = 0;
        kebabiste.orderedIngredients.Add(ingredient);
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
            if (cpt >= kebabiste.orderingTime)
            {
                break;
            }
        }
        if (kebabiste.money >= ingredientPrices[ingredient])
        {
            kebabiste.money -= ingredientPrices[ingredient];
            kebabiste.ingredientAmounts[ingredient] += 5;
        }

        kebabiste.orderedIngredients.Remove(ingredient);
    }
    public IEnumerator PrepareIngredient(Ingredient ingredient, Kebabiste kebabiste)
    {
        int cpt = 0;
        kebabiste.preparation = true;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
            if (cpt >= kebabiste.preparationTime)
            {
                break;
            }
        }
        if (kebabiste.ingredientAmounts[ingredient] > 0)
        {
            kebabiste.ingredientAmounts[ingredient] -= 1;
            kebabiste.ingredientsReadyToUse[ingredient] += 1;
        }

        kebabiste.preparation = false;
    }

    public IEnumerator Corrupt(Kebabiste kebabiste)
    {
        if (kebabiste.money >= 500)
        {
            kebabiste.corrupting = true;
            kebabiste.money -= 500;
            int cpt = 0;
            kebabiste.opponent.orderingTime += 4;
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                cpt++;
                if (cpt >= kebabiste.corruptionTime)
                {
                    break;
                }
            }

            kebabiste.opponent.orderingTime -= 4;
            kebabiste.corrupting = false;
        }
    }
    public void FakeCustomer(Kebabiste kebabiste)
    {
        if (kebabiste.money >= 500)
        {
            kebabiste.opponent.fakeCustomerInQueue = true;
            kebabiste.money -= 500;
        }
    }
    public IEnumerator TakeBreak(Kebabiste kebabiste)
    {
        kebabiste.unableToAct = true;
        int cpt = 0;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
            if (cpt >= kebabiste.restTime)
            {
                break;
            }
        }
        kebabiste.stress = 0;
        kebabiste.unableToAct = false;
    }
    public IEnumerator CreateDish(Kebabiste kebabiste)
    {
        kebabiste.serving = true;
        foreach (Ingredient ingredient in kebabiste.recipe)
        {
            if (kebabiste.ingredientsReadyToUse[ingredient] <= 0)
            {
                Debug.Log("missing " +ingredient);
                kebabiste.serving = false;
                yield break;
            }
        }
        int cpt = 0;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
            if (cpt >= kebabiste.servingTime)
            {
                break;
            }
        }
        foreach (Ingredient ingredient in kebabiste.recipe)
        {
            kebabiste.ingredientsReadyToUse[ingredient] -= 1;
        }

        kebabiste.recipe = null;
        
        if (!kebabiste.customers.First().isFake)
        {
            kebabiste.servedCount += 1;
            kebabiste.money += 200;
            kebabiste.serving = false;
        }
        else
        {
            kebabiste.stress += 20;
        }
        kebabiste.customers.Remove(kebabiste.customers.First());
        Debug.Log("CustomerServed");
    }
}
