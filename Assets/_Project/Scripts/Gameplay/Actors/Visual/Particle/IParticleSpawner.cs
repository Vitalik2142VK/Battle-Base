namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public interface IParticleSpawner
    {
        public IParticle Spawn(string particleId);
    }
}