using BattleBase.Gameplay.Actors.Types;
using BattleBase.Gameplay.Levels;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BaseInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private Base[] _bases;

        private IActorComposer _composer;
        private IWinStateController _winStateController;

        [Inject]
        public void Construct(IActorComposer composer, IWinStateController winStateController)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
        }

        private void Start()
        {
            foreach (var baseView in _bases)
                InitBase(baseView);
        }

        private void InitBase(Base view)
        {
            Actor actor = _composer.Compose(view, _config, view.Team);
            _winStateController.AddBase(actor);
        }
    }
}