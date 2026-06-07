using BattleBase.Gameplay.Map;
using BattleBase.SaveService;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Colored
{
    public class ColorGetter : MonoBehaviour, IColorGetter
    {
        [SerializeField] private ColorSetConfig _colorSetConfig;

        private IColorData _colorData;

        [Inject]
        public void Construct(IColorSaver colorSaver) =>
            _colorData = colorSaver.ColorData ?? throw new ArgumentNullException(nameof(colorSaver));

        public Color GetTeamColor(TeamType teamType)
        {
            int colorIndex;

            if (teamType == TeamType.Player)
                colorIndex = _colorData.PlayerColorIndex;
            else
                colorIndex = _colorData.EnemyColorIndex;

            return _colorSetConfig.Colors[colorIndex];
        }
    }
}