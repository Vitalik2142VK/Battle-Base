using System.Collections.Generic;
using BattleBase.Abstract;
using BattleBase.Commands;
using UnityEngine;

namespace BattleBase.Bootstraps
{
    public class MenuBootstrap : BootstrapBase
    {
        [SerializeField] private List<MediatorBase> _mediators;
        [SerializeField] private List<CommandBase> _commandsToStart;

        private void Start()
        {
            foreach (MediatorBase mediator in _mediators)
                mediator.Init();

            foreach (CommandBase command in _commandsToStart)
                command.Execute();
        }
    }
}