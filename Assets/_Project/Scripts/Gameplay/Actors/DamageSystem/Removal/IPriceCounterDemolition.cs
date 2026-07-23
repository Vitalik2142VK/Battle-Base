namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public interface IPriceCounterDemolition : IDemolitionData
    {
        public void Enable();

        public void Disable();
    }
}