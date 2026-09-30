namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public interface ISoundEffectSpawner
    {
        public ISoundEffect Spawn(string particleId);
    }
}