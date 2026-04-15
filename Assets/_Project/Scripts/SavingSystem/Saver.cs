using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.SaveService
{
    public class Saver : IAudioVolumeSaver, IColorSaver, ITerritorySaver
    {
        private readonly ISaveSystem _saveSystem;

        public Saver(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem ?? throw new ArgumentNullException(nameof(saveSystem));
        }

        public float GeneralVolume => Data.GeneralVolume;

        public float MusicVolume => Data.MusicVolume;

        public float SfxVolume => Data.SfxVolume;

        public int PlayerColorIndex => Data.PlayerColorIndex;

        public int EnemyColorIndex => Data.EnemyColorIndex;

        private SavesData Data => _saveSystem.Data;

        public IReadOnlyList<int> ConqueredTerritories => Data.ConqueredTerritories;

        public void Save() =>
            _saveSystem.SaveProgress();

        public void SetGeneralVolume(float volume) =>
            Data.GeneralVolume = Mathf.Clamp01(volume);

        public void SetMusicVolume(float volume) =>
            Data.MusicVolume = Mathf.Clamp01(volume);

        public void SetSfxVolume(float volume) =>
            Data.SfxVolume = Mathf.Clamp01(volume);

        public void SetPlayerColorIndex(int index) =>
            Data.PlayerColorIndex = index;

        public void SetEnemyColorIndex(int index) =>
            Data.EnemyColorIndex = index;

        public void SetConqueredTerritories(IReadOnlyList<int> territories) =>
            Data.ConqueredTerritories = new(territories);
    }
}