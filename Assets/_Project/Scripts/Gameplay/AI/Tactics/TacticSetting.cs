using BattleBase.Gameplay.AI.Deactevators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics
{
    public abstract class TacticSetting : ScriptableObject
    {
        [Header("Common")]
        [SerializeField] private TacticDeactivatorScore[] _deactivatorScores;
        [SerializeField][Range(1, 100)] private int _maxScore = 50;
        [SerializeField][Range(0, 50)] private int _minScore = 0;

        private void OnValidate()
        {
            if (_minScore > _maxScore)
                _minScore = _maxScore;
        }

        public virtual IEnumerable<ITacticDeactivatorScore> DeactivatorScores => _deactivatorScores;

        public virtual int MaxScore => _maxScore;

        public virtual int MinScore => _minScore;
    }
}