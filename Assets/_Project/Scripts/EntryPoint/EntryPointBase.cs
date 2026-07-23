using System;
using System.Collections.Generic;
using BattleBase.AudioService;
using BattleBase.Commands;
using BattleBase.Mediators;
using UnityEngine;
using VContainer;

namespace BattleBase.EntryPoints
{
    public abstract class EntryPointBase : MonoBehaviour
    {
        [SerializeField] private List<MediatorBase> _mediators;
        [SerializeField] private List<CommandBase> _commandsToStart;

        private AudioVolumeService _volumeService;

        [Inject]
        public void Construct(AudioVolumeService volumeService) =>
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));

        protected virtual void Start()
        {
            foreach (MediatorBase mediator in _mediators)
                mediator.Init();

            foreach (CommandBase command in _commandsToStart)
                command.Execute();

            _volumeService.UpdateVolume();
        }
    }
}