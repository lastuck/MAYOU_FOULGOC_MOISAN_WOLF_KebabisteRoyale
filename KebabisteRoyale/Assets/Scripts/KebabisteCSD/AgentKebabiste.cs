using System;
using System.Threading.Tasks;
using BehaviorTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KebabisteCSD
{
    public class AgentKebabiste : Kebabiste
    {
        private Selector selector;
        private KebabisteIntent kebabisteIntent;

        public AgentKebabiste()
        {
            selector = new Selector();
            
            // Init different sequences

            // Si le stress est trop haut
            SequenceAction sequenceTakeBreak = new SequenceAction(() => 
                SetIntent(new KebabisteIntent {
                    action = Action.TakeBreak
                })
            );
            SequenceCondition sequenceCheckStress = new SequenceCondition(CheckStressCondition, sequenceTakeBreak);

            selector.AddNode(sequenceCheckStress);

            // Si il manque des ingrédients pour la recette
            CreateSequenceForRecipe();

            // Si l'adversaire à un niveau de stress élevé ou il est trop en avance
            SequenceAction sequenceAttackOpponent = new SequenceAction(() =>
            {
                Action attackAction = Random.Range(0, 2) == 0 ? Action.FakeClient : Action.Corrupt;
                SetIntent(new KebabisteIntent
                {
                    action = attackAction
                });
            });
            SequenceCondition sequenceOpponentCondition =
                new SequenceCondition(CheckOpponentStressCondition, sequenceAttackOpponent);

            selector.AddNode(sequenceOpponentCondition);
            
            // Si le seuil d'ingredient préparé est trop bas
            CreateSequencePrepareIngredient();
            
            // Si un plat est attendu et peut être préparé
            SequenceAction sequencePrepareRecipe = new SequenceAction(() => 
                SetIntent(new KebabisteIntent {
                    action = Action.CreateDish
                })
            );
            SequenceCondition sequenceRecipeCanBeMade =
                new SequenceCondition(CheckRecipeCanBeMade, sequencePrepareRecipe);

            selector.AddNode(sequenceRecipeCanBeMade);

            // action par defaut;
            NodeAlwaysTrue defaultSequence = new NodeAlwaysTrue();
            
            selector.AddNode(defaultSequence);
        }

        private void CreateSequenceForRecipe()
        {
            foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
            {
                SequenceAction sequenceOrderIngredient = new SequenceAction(() => 
                    SetIntent(new KebabisteIntent {
                        action = Action.OrderIngredient,
                        ingredient = ing
                    })
                );
                SequenceCondition sequenceMissingIngredients =
                    new SequenceCondition(() => CheckMissingIngredient(ing), sequenceOrderIngredient);

                selector.AddNode(sequenceMissingIngredients);
            }
        }

        private void CreateSequencePrepareIngredient()
        {
            foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
            {
                SequenceAction sequencePrepareIngredient = new SequenceAction(() => 
                    SetIntent(new KebabisteIntent {
                        action = Action.PrepareIngredient,
                        ingredient = ing
                    })
                );
                SequenceCondition sequenceMissingPrepareIngredients =
                    new SequenceCondition(() => CheckMissingPrepareIngredient(ing), sequencePrepareIngredient);

                selector.AddNode(sequenceMissingPrepareIngredients);
            }
        }

        private bool CheckRecipeCanBeMade()
        {
            if (unableToAct || recipe == null)
            {
                return false;
            }

            foreach (Ingredient ingredient in recipe)
            {
                if (ingredientsReadyToUse[ingredient] == 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        private bool CheckOpponentStressCondition()
        {
            return (opponent.stress >= MAX_STRESS - 10 || opponent.servedCount > servedCount + 2) && money > 500 && !unableToAct;
        }

        private bool CheckStressCondition()
        {
            return stress >= MAX_STRESS - 10 && !unableToAct;
        }
        
        private bool CheckMissingIngredient(Ingredient ing)
        {
            if (unableToAct || recipe == null || !recipe.Contains(ing))
            {
                return false;
            }

            if (ingredientAmounts[ing] + ingredientsReadyToUse[ing] < 2 && !orderedIngredients.Contains(ing) && GameController.ingredientPrices[ing] < money)
            {
                return true;
            }

            return false;
        }

        private bool CheckMissingPrepareIngredient(Ingredient ing)
        {
            if (unableToAct)
            {
                return false;
            }

            if (ingredientAmounts[ing] > 0 && ingredientsReadyToUse[ing] <= 1)
            {
                return true;
            }

            return false;
        }

        private void SetIntent(KebabisteIntent intent)
        {
            string wantTo = "Want to " + intent.action;

            if (intent.action == Action.OrderIngredient || intent.action == Action.PrepareIngredient)
            {
                wantTo += " " + intent.ingredient;
            }

            Debug.Log(wantTo);
            kebabisteIntent = intent;
        }

        public async Task ComputeIntent()
        {
            while (!GameController.endOfBehavior && GameController.gameRunning && Application.isPlaying)
            {
                if (kebabisteIntent == null)
                {
                    await selector.CheckCondition();
                }

                await Task.Delay(10);
            }
        }

        public override KebabisteIntent GetIntent()
        {
            KebabisteIntent toReturn = kebabisteIntent;
            kebabisteIntent = null;
            return toReturn;
        }
    }
}