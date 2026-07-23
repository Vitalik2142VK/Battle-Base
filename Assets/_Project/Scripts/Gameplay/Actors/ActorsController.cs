using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Energy;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors
{
    public class ActorsController : MonoBehaviour, IActorsController
    {
        private List<IActor> _activeActors;
        private IAdvancedPowerRegistry _powerRegistry;

        private void Awake()
        {
            _activeActors = new List<IActor>();
        }

        [Inject]
        public void Construct(IAdvancedPowerRegistry powerRegistry)
        {
            _powerRegistry = powerRegistry ?? throw new System.ArgumentNullException(nameof(powerRegistry));
        }

        private void OnDisable()
        {
            foreach (var actor in _activeActors)
            {
                if (actor.TryGetComponent(out IOnTimeDestroyable destroyable))
                    destroyable.Destroy();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _activeActors.Count; i++)
            {
                IActor actor = _activeActors[i];

                if (actor.IsEnabled)
                {
                    actor.Update(Time.fixedDeltaTime);
                }
                else
                {
                    _powerRegistry.Release(actor.TeamType, actor.Data);
                    _activeActors.RemoveAt(i--);
                }
            }
        }

        public void AddActor(IActor actor)
        {
            if (actor == null)
                throw new System.ArgumentNullException(nameof(actor));

            _activeActors.Add(actor);
        }
    }
}
