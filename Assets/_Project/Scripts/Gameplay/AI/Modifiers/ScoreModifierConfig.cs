using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public abstract class ScoreModifierConfig : ScriptableObject
    {
        [SerializeField] private Modifier[] _modifiers;

        public IEnumerable<IModifier> Modifiers => _modifiers;

        public abstract ModifierType Type { get; }
    }
}