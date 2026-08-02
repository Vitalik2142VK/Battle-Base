using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers
{
    [System.Serializable]
    public class Modifier : IModifier
    {
        [SerializeField] private TacticCategory _category;
        [SerializeField][Range(0f, 5f)] private float _multiplier = 1f;

        public TacticCategory Category => _category;

        public float Multiplier => _multiplier;
    }
}