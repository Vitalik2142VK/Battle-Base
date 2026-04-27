using BattleBase.Gameplay.Movement;
using UnityEngine;

namespace BattleBase.Gameplay.Units 
{
    [RequireComponent(typeof(Mover))]
    public class UnitView : MonoBehaviour
    {
        private IMover _mover;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void Start()
        {
            _mover.Move();
        }
    }
}
