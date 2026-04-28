using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using UnityEngine;

namespace BattleBase.Gameplay.Spawn
{
    [RequireComponent(typeof(Renderer))]
    public class UnitFactory : MonoBehaviour, IFactory<Unit>
    {
        [SerializeField] private Unit _prefab;
        [SerializeField] private SideUnit _side;
        
        private Renderer _renderer;
        private int _spawnedUnits;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _spawnedUnits = 0;
        }

        public Unit Create()
        {
            string name = $"{_side}_{_prefab.name}_{_spawnedUnits++}";
            Unit unit = Instantiate(_prefab, transform);
            unit.SetSide(_side);
            unit.gameObject.SetActive(false);
            unit.gameObject.name = name;

            var renderersUnit = GetComponentsInChildren<MeshRenderer>(true);

            foreach (var renderer in renderersUnit)
                renderer.material = _renderer.material;

            return unit;
        }
    }
}