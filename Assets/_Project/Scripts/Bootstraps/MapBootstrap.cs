using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Map;
using BattleBase.Mediators;
using UnityEngine;

namespace BattleBase.Bootstraps
{
    public class MapBootstrap : BootstrapBase
    {
        [SerializeField] private List<MediatorBase> _mediators;
        [SerializeField] private List<CommandBase> _comandsToStart;
        [SerializeField] private List<Territory> _territories;        
        [SerializeField] private MapColorMediator _colorMediator;

        private void Start()
        {
            foreach (MediatorBase mediator in _mediators)
                mediator.Init();

            foreach (CommandBase command in _comandsToStart)
                command.Execute();
        }
    }
}