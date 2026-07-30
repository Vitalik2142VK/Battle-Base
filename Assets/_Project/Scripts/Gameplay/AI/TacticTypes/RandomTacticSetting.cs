using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.TacticTypes
{
    [CreateAssetMenu(fileName = nameof(RandomTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(RandomTacticSetting))]
    public class RandomTacticSetting : TacticSetting, IRandomTacticSetting
    {
        [SerializeField][Range(2, 5)] private int _maxNumSpawn = 3;
        [SerializeField][Range(1, 3)] private int _minNumSpawn = 1;

        private void OnValidate()
        {
            if (_maxNumSpawn < _minNumSpawn)
                _minNumSpawn = _maxNumSpawn - 1;
        }

        public TacticCategory Category => TacticCategory.No;

        public int MaxNumSpawn => _maxNumSpawn;

        public int MinNumSpawn => _minNumSpawn;
    }
}