using BattleBase.Gameplay.Actors.AttackSystem.Aim;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    [PreviewExcluded(PreviewExclusionMode.Remove)]
    public class MultyAim : MonoBehaviour, IMultyAim
    {
        [SerializeField][SerializeIterface(typeof(IAim))] private GameObject[] _gameObjectAims;

        private List<IAim> _aims;

        public IEnumerable<IAim> AdditionalAims => _aims;

        private void Awake()
        {
            _aims = new List<IAim>();

            foreach (var gameObject in _gameObjectAims)
            {
                if (gameObject == null)
                    continue;

                IAim aim = gameObject.GetComponent<IAim>();

                if (aim == null)
                    continue;

                _aims.Add(aim);
            }
        }
    }
}