using System;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public interface ISoundEffectSpawner
    {
        public event Action<string> Spawned;

        public bool TrySpawn(string particleId, out ISoundEffect soundEffect);
    }
}