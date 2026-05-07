using System;

namespace BattleBase.Gameplay.MiniMap
{
    public interface IMiniMapObjectRegistry
    {
        public event Action Changed;

        public void Register(IMiniMapTrackable trackable);

        public void Unregister(IMiniMapTrackable trackable);
    }
}