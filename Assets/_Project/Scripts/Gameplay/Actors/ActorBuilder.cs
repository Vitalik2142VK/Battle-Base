using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class ActorBuilder
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private IActorView _view;
        private IActorData _actorData;
        private IDamagebleEvents _damagebleEvent;

        public ActorBuilder()
        {
            _components = new Dictionary<Type, IActorComponent>();
        }

        public ActorBuilder ActorView(IActorView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            return this;
        }

        public ActorBuilder ActorData(IActorData actorData)
        {
            _actorData = actorData ?? throw new ArgumentNullException(nameof(actorData));

            return this;
        }

        public ActorBuilder DamagebleEvents(IDamagebleEvents damagebleEvent)
        {
            _damagebleEvent = damagebleEvent ?? throw new ArgumentNullException(nameof(damagebleEvent));

            return this;
        }

        public ActorBuilder AddComponent<T>(T component) where T : class, IActorComponent
        {
            if (typeof(T).IsInterface == false)
                throw new InvalidOperationException($"Use interface type instead of {typeof(T)}");

            Type heir = FindHeir(component);

            _components[heir] = component;

            UnityEngine.Debug.Log($"Components: actor.IHealth == {typeof(T)}");

            return this;
        }

        public Actor Build()
        {
            return new Actor(_components, _view, _actorData, _damagebleEvent);
        }

        private Type FindHeir(IActorComponent component)
        {
            var interfaces = component.GetType().GetInterfaces();

            foreach (var @interface in interfaces)
            {
                if (typeof(IActorComponent).IsAssignableFrom(@interface))
                {
                    return @interface;
                }
            }

            return typeof(IActorComponent);
        }
    }
}
