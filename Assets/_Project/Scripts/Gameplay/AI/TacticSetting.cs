using UnityEngine;

namespace BattleBase.Gameplay.AI
{
    public abstract class TacticSetting : ScriptableObject
    {
        [SerializeField][Range(1, 100)] private int _defaultScore = 50;

        public virtual int Score => _defaultScore;
    }
}