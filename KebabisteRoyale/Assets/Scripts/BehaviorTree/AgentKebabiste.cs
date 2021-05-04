using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviorTree
{
    public class AgentKebabiste : Kebabiste
    {
        private Selector<KebabisteIntent> selector;
        private KebabisteIntent kebabisteIntent;

        public AgentKebabiste()
        {
            selector = new Selector<KebabisteIntent>();
            
            // Init different sequences

            // Si le stress est trop haut
            SequenceAction<KebabisteIntent> sequenceTakeBreak = new SequenceAction<KebabisteIntent>(() => 
                new KebabisteIntent {
                    action = Action.TakeBreak
                }
            );
            SequenceCondition<KebabisteIntent> sequenceCheckStress = new SequenceCondition<KebabisteIntent>(CheckStressCondition, sequenceTakeBreak);

            selector.AddNode(sequenceCheckStress);

            // Si il manque des ingrédients
            foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
            {
                SequenceAction<KebabisteIntent> sequenceOrderIngredient = new SequenceAction<KebabisteIntent>(() => 
                    new KebabisteIntent {
                        action = Action.OrderIngredient,
                        ingredient = ing
                    }
                );
                SequenceCondition<KebabisteIntent> sequenceMissingIngredients =
                    new SequenceCondition<KebabisteIntent>(() => CheckMissingIngredient(ing), sequenceOrderIngredient);

                selector.AddNode(sequenceMissingIngredients);
            }
            
            // Si l'adversaire à un niveau de stress élevé ou il est trop en avance
            SequenceAction<KebabisteIntent> sequenceAttackOpponent = new SequenceAction<KebabisteIntent>(() =>
            {
                Action attackAction = Random.Range(0, 2) == 0 ? Action.FakeClient : Action.Corrupt;
                return new KebabisteIntent
                {
                    action = attackAction
                };
            });
            SequenceCondition<KebabisteIntent> sequenceOpponentCondition =
                new SequenceCondition<KebabisteIntent>(CheckOpponentStressCondition, sequenceAttackOpponent);

            selector.AddNode(sequenceOpponentCondition);
            
            // Si le seuil d'ingredient préparé est trop bas
            foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
            {
                SequenceAction<KebabisteIntent> sequencePrepareIngredient = new SequenceAction<KebabisteIntent>(() => 
                    new KebabisteIntent {
                        action = Action.PrepareIngredient,
                        ingredient = ing
                    }
                );
                SequenceCondition<KebabisteIntent> sequenceMissingPrepareIngredients =
                    new SequenceCondition<KebabisteIntent>(() => CheckMissingPrepareIngredient(ing), sequencePrepareIngredient);

                selector.AddNode(sequenceMissingPrepareIngredients);
            }
            
            // Si un plat est attendu et peut être préparé
            SequenceAction<KebabisteIntent> sequencePrepareRecipe = new SequenceAction<KebabisteIntent>(() => 
                new KebabisteIntent {
                    action = Action.CreateDish
                }
            );
            SequenceCondition<KebabisteIntent> sequenceRecipeCanBeMade =
                new SequenceCondition<KebabisteIntent>(CheckRecipeCanBeMade, sequencePrepareRecipe);

            selector.AddNode(sequenceRecipeCanBeMade);

            // Si un plat est prêt à être servi
            // TODO : Servir un plat une fois prêt
            
            // action par defaut;
            SequenceAction<KebabisteIntent> defaultAction = new SequenceAction<KebabisteIntent>(() => null);
            NodeAlwaysTrue<KebabisteIntent> defaultSequence = new NodeAlwaysTrue<KebabisteIntent>(defaultAction);
            
            selector.AddNode(defaultSequence);
        }

        private bool CheckRecipeCanBeMade()
        {
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
            return (opponent.stress >= MAX_STRESS - 10 || opponent.servedCount > servedCount + 2) && money > 100;
        }

        private bool CheckStressCondition()
        {
            return stress >= MAX_STRESS - 10;
        }
        
        private bool CheckMissingIngredient(Ingredient ing)
        {
            if (ingredientAmounts[ing] <= 2 && GameController.ingredientPrices[ing] < money)
            {
                Debug.Log("Il manque " + ing);
                return true;
            }

            return false;
        }

        private bool CheckMissingPrepareIngredient(Ingredient ing)
        {
            if (ingredientAmounts[ing] > 0 && ingredientsReadyToUse[ing] <= 2)
            {
                Debug.Log("Il manque " + ing);
                return true;
            }

            return false;
        }

        public async Task ComputeIntent()
        {
            while (!GameController.endOfBehavior)
            {
                selector.GetSelectorResult();
                await Task.Delay(100);
            }
        }

        public override KebabisteIntent GetIntent()
        {
            return kebabisteIntent;
        }
    }
}