using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    [CreateAssetMenu(
        fileName = nameof(MaterialConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(MaterialConfig))]
    public class MaterialConfig : ActorComponentSource, IMaterialConfig
    {
        [SerializeField][Range(10000, 100000)] private int _maxCapacity = 99900;

        public int MaxCapacity => _maxCapacity;
    }
}