namespace BattleBase.Gameplay.Actors.Visual
{
    public interface ITrailParticleSpawner
    {
        public ITrailParticle Spawn(string trailParticleId);
    }
}