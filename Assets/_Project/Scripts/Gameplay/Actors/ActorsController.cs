using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Energy;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors
{
    public class ActorsController : MonoBehaviour, IActorsController, IActorsStorage
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
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
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

                    int lastIndex = _activeActors.Count - 1;
                    _activeActors[i] = _activeActors[lastIndex];
                    _activeActors.RemoveAt(lastIndex);
                    i--;
                }
            }
        }

        public void AddActor(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            _activeActors.Add(actor);
        }

        public int GetActorPositionsOtherTeam(IActorPosition[] positions, TeamType team)
        {
            if (positions == null)
                throw new ArgumentNullException(nameof(positions));

            if (positions.Length == 0)
                return 0;

            int index = 0;

            foreach (var actor in _activeActors)
            {
                if (actor.TeamType != team)
                    positions[index++] = actor.Position;

                if (index >= positions.Length)
                    break;
            }

            if (index < positions.Length)
            {
                for (int i = index; i < positions.Length; i++)
                {
                    positions[i] = null;
                }
            }

            return index;
        }
    }
}
