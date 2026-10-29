using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public abstract class ScoreModifierConfig : ScriptableObject
    {
        [SerializeField] private Modifier[] _modifiers;
        [SerializeField][Min(1)] private int _priority = 1;

        public IEnumerable<IModifier> Modifiers => _modifiers;

        public int Priority => _priority;

        public abstract ModifierType Type { get; }
    }
}