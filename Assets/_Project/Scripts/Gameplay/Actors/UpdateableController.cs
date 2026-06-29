using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors
{
    public class UpdateableController : IUpdateableController
    {
        private readonly List<IUpdateable> _components;

        public UpdateableController(IEnumerable<IActorComponent> components)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            _components = components
                .OfType<IUpdateable>()
                .ToList();
        }

        public void AddComponent(IActorComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component is IUpdateable updateable)
                _components.Add(updateable);
        }

        public void Update(float delta)
        {
            foreach (var component in _components)
                component.Update(delta);
        }
    }
}
