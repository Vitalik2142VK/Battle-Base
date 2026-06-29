using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.AI;
using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Utils;

namespace BattleBase.Gameplay.Actors
{
    public class ActorBuilder
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private IActorView _view;
        private IProductionData _actorData;
        private IDestroyableEvents _destroyableEvents;

        public ActorBuilder()
        {
            _components = new Dictionary<Type, IActorComponent>();
        }

        public ActorBuilder ActorView(IActorView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            return this;
        }

        public ActorBuilder ActorData(IProductionData actorData)
        {
            _actorData = actorData ?? throw new ArgumentNullException(nameof(actorData));

            return this;
        }

        public ActorBuilder DamagebleEvents(IDestroyableEvents damagebleEvent)
        {
            _destroyableEvents = damagebleEvent ?? throw new ArgumentNullException(nameof(damagebleEvent));

            return this;
        }

        public ActorBuilder AddComponent<T>(T component) where T : class, IActorComponent
        {
            if (component == null) 
                throw new ArgumentNullException(nameof(component));

            if (typeof(T).IsInterface == false)
                throw new InvalidOperationException($"Use interface type instead of {typeof(T)}");

            Type heir = TypeTools.FindDerivedInterface<IActorComponent>(component);

            _components[heir] = component;

            return this;
        }

        public Actor Build()
        {
            if (_destroyableEvents == null)
                AddOnTimeDamageble();

            return new Actor(_components, _view, _actorData, _destroyableEvents);
        }

        private void AddOnTimeDamageble()
        {
            IOnTimeDestroyable onTimeDamageble = new OnTimeDestroyable();
            _destroyableEvents = onTimeDamageble;

            AddComponent(onTimeDamageble);
        }
    }
}
