using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI
{
    [CreateAssetMenu(
    fileName = nameof(BrainConfing),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing))]
    public class BrainConfing : ScriptableObject, IBrainConfing
    {
        [SerializeField] private TacticType[] _usedTactics;
        [SerializeField] private TeamType _teamType;

        public IEnumerable<TacticType> UsedTacticTypes => _usedTactics;

        public TeamType TeamType => _teamType;
    }
}