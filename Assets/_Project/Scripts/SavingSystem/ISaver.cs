using System;

namespace BattleBase.SaveService
{
    public interface ISaver : IAudioVolumeSaver, IColorSaver, ITerritorySaver, IShopSaver
    {
        public event Action ProgressReseted;

        public void SaveProgress();

        public void ResetProgress();
    }
}