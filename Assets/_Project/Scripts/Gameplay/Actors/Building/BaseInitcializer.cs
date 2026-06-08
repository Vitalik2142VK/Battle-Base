using BattleBase.Gameplay.Actors.Types;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BaseInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private Base _playerBase;
        [SerializeField] private Base _enemyBase;

        private IActorComposer _composer;

        [Inject]
        public void Construct(
            IActorComposer composer)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        private void Start()
        {
            InitBase(_playerBase);
            InitBase(_enemyBase);
        }

        private void InitBase(Base view) => 
            _composer.Compose(view, _config, view.Team);
    }
}