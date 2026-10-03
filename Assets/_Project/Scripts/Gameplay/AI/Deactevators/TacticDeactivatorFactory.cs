using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public class TacticDeactivatorFactory : ITacticDeactivatorFactory
    {
        private readonly Dictionary<Type, IDeactivatorFactory> _factories;

        public TacticDeactivatorFactory(IEnumerable<IDeactivatorFactory> factories)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories));

            _factories = new Dictionary<Type, IDeactivatorFactory>();

            foreach (var factory in factories)
                _factories.Add(factory.DeactevatorType, factory);
        }

        public ITacticDeactivator Create(IEnumerable<ITacticDeactivatorScore> scores)
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));

            TacticDeactivator tacticDeactevator = new();

            foreach (var score in scores)
            {
                if (_factories.TryGetValue(score.Type, out IDeactivatorFactory factory) == false)
                    throw new InvalidOperationException($"There is no factory that produces type - '{nameof(score.Type)}'");

                IDeactivator deactevator = factory.Create(score);
                tacticDeactevator.AddDeactevator(deactevator);
            }

            return tacticDeactevator;
        }
    }
}