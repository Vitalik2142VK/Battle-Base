using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Utils;
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

#if UNITY_EDITOR
        private int _countActorBeforInit; //todo remove on release 
        private bool _isFirsFrame = false;
#endif

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

        private void LateUpdate()
        {
#if UNITY_EDITOR //todo remove on release 
            if (_isFirsFrame == false)
            {
                _isFirsFrame = true;
                _countActorBeforInit = _activeActors.Count;
            }

            if (DebugSetting.IsShowCountActor) 
                Debug.Log($"Count active Actors = {_activeActors.Count - _countActorBeforInit}");
#endif
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
                    positions[i] = null;
            }

            return index;
        }
    }
}
