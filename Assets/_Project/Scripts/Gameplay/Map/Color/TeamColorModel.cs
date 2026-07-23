using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Actors;
using BattleBase.SaveService;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TeamColorModel : ISaveable
    {
        public static float LightenFactor = 0.3f;

        private readonly IColorSaver _saver;
        private readonly List<Color> _colors;

        public TeamColorModel(IColorSaver saver, TeamColorSetConfig config)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _colors = new (config.Colors);

            Load();
        }

        public event Action Changed;

        public IReadOnlyList<Color> Colors => _colors;

        public int PlayerColorIndex { get; private set; }

        public int EnemyColorIndex { get; private set; }

        public Color PlayerColor => _colors[PlayerColorIndex];

        public Color EnemyColor => _colors[EnemyColorIndex];

        public void SetPlayerColorIndex(int index)
        {
            if (index < 0 || index >= _colors.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            PlayerColorIndex = index;

            Changed?.Invoke();
        }

        public void SetEnemyColorIndex(int index)
        {
            if (index < 0 || index >= _colors.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            EnemyColorIndex = index;

            Changed?.Invoke();
        }

        public Color GetColor(TeamType teamType)
        {
            return teamType switch
            {
                TeamType.Player => PlayerColor,
                TeamType.Enemy => EnemyColor,
                _ => throw new NotImplementedException(),
            };
        }

        public void Load()
        {
            PlayerColorIndex = _saver.ColorData.PlayerColorIndex;
            EnemyColorIndex = _saver.ColorData.EnemyColorIndex;

            Changed?.Invoke();
        }

        public void Save()
        {
            ColorData data = new(PlayerColorIndex, EnemyColorIndex);
            _saver.SetColorData(data);
        }
    }
}