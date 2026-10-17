using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Types
{
    public class CombatUnit : ActorView
    {
        private void Reset()
        {
            if (gameObject.TryGetComponent(out HealthView _) == false)
                gameObject.AddComponent<HealthView>();

            if (gameObject.TryGetComponent(out AttackerView _) == false)
                gameObject.AddComponent<AttackerView>();

            if (gameObject.TryGetComponent(out MoverView _) == false)
                gameObject.AddComponent<MoverView>();

            if (gameObject.TryGetComponent(out NavigationAgent _) == false)
                gameObject.AddComponent<NavigationAgent>();

            if (gameObject.TryGetComponent(out TargetFinder _) == false)
                gameObject.AddComponent<TargetFinder>();

            if (gameObject.TryGetComponent(out ColoredActorView _) == false)
                gameObject.AddComponent<ColoredActorView>();

            if (gameObject.GetComponentInChildren<Target>() == null)
            {
                GameObject target = new();
                target.transform.parent = transform;
                target.AddComponent<Target>();
                target.name = nameof(Target);
            }
        }
    }
}