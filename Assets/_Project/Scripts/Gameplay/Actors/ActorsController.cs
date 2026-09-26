using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public class ActorsController : MonoBehaviour, IActorsController, IActorsStorage
    {
        private HashSet<IActor> _activeActors;
        private HashSet<IActor> _newActors;
        private List<IActor> _disableActors;

#if UNITY_EDITOR
        private int _countActorBeforInit; //todo remove on release 
        private bool _isFirsFrame = false;
#endif

        private void Awake()
        {
            _activeActors = new HashSet<IActor>();
            _newActors = new HashSet<IActor>();
            _disableActors = new List<IActor>();
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
            foreach (var actor in _activeActors)
            {
                if (actor.IsEnabled)
                    actor.Update(Time.fixedDeltaTime);
                else
                    _disableActors.Add(actor);
            }

            foreach (var actor in _disableActors)
                _activeActors.Remove(actor);

            foreach (var actor in _newActors)
                _activeActors.Add(actor);

            _disableActors.Clear();
            _newActors.Clear();
        }

        private void LateUpdate()
        {
#if UNITY_EDITOR //todo remove on release 
            if (_isFirsFrame == false)
            {
                _isFirsFrame = true;
                _countActorBeforInit = _activeActors.Count;
            }

            if (DebugSettingGameScene.IsShowCountActor)
                Debug.Log($"Count active Actors = {_activeActors.Count - _countActorBeforInit}");
#endif
        }

        public void AddActor(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            _newActors.Add(actor);
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
