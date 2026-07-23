namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public interface ITrailParticleSpawner
    {
        public ITrailParticle Spawn(string trailParticleId);
    }
}