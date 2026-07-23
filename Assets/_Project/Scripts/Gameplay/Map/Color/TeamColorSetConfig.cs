using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(TeamColorSetConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(TeamColorSetConfig))]
    public class TeamColorSetConfig : ScriptableObject
    {
        [SerializeField] private List<Color> _colors;

        public IReadOnlyList<Color> Colors => _colors;
    }
}