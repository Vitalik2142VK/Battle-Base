using UnityEngine;

namespace BattleBase.Gameplay.HealthSystem
{
    public class ConsoleHealthBar : IHealthBar
    {
        private readonly string _nameObject;

        private float _maxHealth;

        public ConsoleHealthBar(string name)
        {
            _nameObject = name ?? throw new System.ArgumentNullException(nameof(name));
        }

        public bool IsActive => false;

        public void SetActive(bool isActive)
        {
            Debug.Log($"{nameof(ConsoleHealthBar)} active is {nameof(isActive)}");
        }

        public void SetMaxHealth(float health)
        {
            _maxHealth = health;
        }

        public void UpdateDataHealth(float health)
        {
            Debug.Log($"Health '{_nameObject}': {_maxHealth}/{health}");
        }
    }
}