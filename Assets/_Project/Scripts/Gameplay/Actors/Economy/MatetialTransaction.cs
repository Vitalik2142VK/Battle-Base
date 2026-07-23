using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MatetialTransaction
    {
        private readonly Material _material;
        private readonly int _reservedMaterials;

        private Action _action;

        public MatetialTransaction(Material material, int reservedMaterials)
        {
            if (reservedMaterials < 0)
                throw new ArgumentOutOfRangeException(nameof(reservedMaterials));

            _material = material ?? throw new ArgumentNullException(nameof(material));
            _reservedMaterials = reservedMaterials;
        }

        public void Init(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Cancle()
        {
            _material.AddMaterials(_reservedMaterials);
            _action = null;
        }

        public void Finish()
        {
            if (_action == null)
                throw new InvalidOperationException("The transaction was not initialized or cancelled");

            _action();
        }
    }
}