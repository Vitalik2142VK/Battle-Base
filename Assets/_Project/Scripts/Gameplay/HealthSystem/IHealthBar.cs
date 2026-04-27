namespace BattleBase.Gameplay.HealthSystem
{
    public interface IHealthBar
    {
        public bool IsActive { get; }

        public void SetActive(bool isActive);

        public void SetMaxHealth(float health);

        public void UpdateDataHealth(float health);
    }
}