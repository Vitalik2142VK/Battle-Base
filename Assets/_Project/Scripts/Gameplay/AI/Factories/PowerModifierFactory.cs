using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Modifiers.Energy;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class PowerModifierFactory : IScoreModifierFactory
    {
        private readonly IPowerRegistry _powerRegistry;

        public PowerModifierFactory(IPowerRegistry powerRegistry)
        {
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
        }

        public ModifierType Type => ModifierType.Power;

        public IAdvancedScoreModifier Create(IScoreModifierConfig configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            if (configs is IPowerModifierConfig powerModifierConfig == false)
                throw new InvalidOperationException($"{nameof(configs)} is not implemented '{nameof(IPowerModifierConfig)}'");

            IPowerData powerData = _powerRegistry.GetPowerData(team);

            return new PowerScoreModifier(powerModifierConfig, powerData);
        }
    }
}