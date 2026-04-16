using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class MapBootstrap : BootstrapBase, IInjectable
    {
        [SerializeField] private List<Canvas> _canvasList;

        [Inject]
        public void Construct(IUIPointerChecker pointerChecker)
        {
            foreach (Canvas canvas in _canvasList)
                pointerChecker.AddCanvas(canvas);
        }
    }
}