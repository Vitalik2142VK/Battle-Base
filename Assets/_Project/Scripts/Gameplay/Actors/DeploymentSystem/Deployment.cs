using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class Deployment : IDeployment
    {
        private readonly List<IActorComponent> _components;
        private readonly IDeploymentData _data;
        private readonly Timer _timer;

        private bool _isEnabled;
        private bool _isDisabledComponents;

        public event Action Started;
        public event Action Finished;

        public Deployment(IDeploymentData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));

            _components = new List<IActorComponent>();
            _timer = new Timer(_data.DeployTime);
        }

        public Type KeyType => typeof(IDeployment);

        public void Enable()
        {
            _isEnabled = true;
            _isDisabledComponents = false;
        }

        public void Disable() => 
            _isEnabled = false;

        public void Update(float delta)
        {
            if (_isEnabled == false || _timer.IsTimeUp)
                return;

            if (_isDisabledComponents == false)
                DisableComponents();

            _timer.Tick(delta);

            if (_timer.IsTimeUp)
                Finished?.Invoke();
        }

        public void AddDisablingComponent(IActorComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component == this)
                throw new InvalidOperationException("Cant add the current component");

            _components.Add(component);
        }

        public void Activate()
        {
            _timer.SetWaitTime(_data.DeployTime);
            _timer.RestartTimer();

            Started?.Invoke();
        }

        public void EnableComponents()
        {
            if (_isEnabled == false)
                return;

            foreach (var component in _components)
                component.Enable();
        }

        public void DisableComponents()
        {
            foreach (var component in _components)
                component.Disable();

            _isDisabledComponents = true;
        }
    }
}
