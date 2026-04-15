using System;
using System.Collections.Generic;

namespace BattleBase.SaveService
{
    [Serializable]
    public class SavesData
    {
        public float GeneralVolume = 1f;
        public float MusicVolume = 0.6f;
        public float SfxVolume = 0.8f;
        public int PlayerColorIndex = 0;
        public int EnemyColorIndex = 5;
        public List<int> ConqueredTerritories = new() { 0, };
    }
}