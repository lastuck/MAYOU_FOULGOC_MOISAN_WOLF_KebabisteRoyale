using System.Threading.Tasks;
using BehaviorTree;
using UnityEngine;

namespace KebabisteFPS
{
    public class FPSAgentKebabiste : FPSKebabiste
    {
        public Action intent;
        private Selector selector;

        public FPSAgentKebabiste()
        {
            selector = new Selector();
            
            // Si vie inférieur à un montant, va se cacher
            SequenceAction wantToHide = new SequenceAction(() => SetIntent(Action.Hide));
            SequenceCondition sequenceConditionHide = new SequenceCondition(CheckLowLife, wantToHide);
            
            selector.AddNode(sequenceConditionHide);

            // Si ammo inférieur à un montant, va recharger
            SequenceAction wantToReload = new SequenceAction(() => SetIntent(Action.Reload), EndReload);
            SequenceCondition sequenceConditionReload = new SequenceCondition(CheckAmmo, wantToReload);
            
            selector.AddNode(sequenceConditionReload);

            // Si Ok
            Selector findOrShootSelector = new Selector();
            NodeAlwaysTrue shootOrMove = new NodeAlwaysTrue(findOrShootSelector);

            // Si cible en vue, tire
            SequenceAction shootOpponent = new SequenceAction(() => SetIntent(Action.Shoot));
            SequenceCondition sequenceConditionCanShoot = new SequenceCondition(CanShootOpponent, shootOpponent);
            
            findOrShootSelector.AddNode(sequenceConditionCanShoot);

            // Si cible non en vu, se déplacer
            SequenceAction goNearAction = new SequenceAction(() => SetIntent(Action.GoNear));
            NodeAlwaysTrue alwaysTrueGoNear = new NodeAlwaysTrue(goNearAction);
            
            findOrShootSelector.AddNode(alwaysTrueGoNear);
            
            selector.AddNode(shootOrMove);
        }

        private bool EndReload()
        {
            return ammo > 0;
        }
        
        private bool CanShootOpponent()
        {
            Ray ray = new Ray(kebabistePrefab.transform.position, kebabistePrefab.transform.forward);

            RaycastHit hitInfo;
            if (ammo != 0 && Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckAmmo()
        {
            return ammo == 0;
        }

        private bool CheckLowLife()
        {
            return life <= 10;
        }

        private void SetIntent(Action action)
        {
            intent = action;
        }
        
        public override Action GetIntent()
        {
            Action toReturn = intent;
            intent = Action.None;
            return toReturn;
        }

        public override void RotateView()
        {
            kebabistePrefab.transform.LookAt(opponent.kebabistePrefab.transform);
        }

        public async Task ComputeIntent()
        {
            while (Application.isPlaying && FPSGameController.gameRunning)
            {
                if (intent == Action.None)
                {
                    await selector.CheckCondition();
                }

                await Task.Delay(10);
            }
        }
    }
}