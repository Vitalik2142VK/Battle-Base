using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Spawn
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _target;
        [SerializeField] private UnitFactory _factory;

        private Pool<Unit> _pool;

        private void OnValidate()
        {
            if (_container == null)
                _container = transform;

            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        private void Awake()
        {
            _pool = new Pool<Unit>(_factory);
        }

        public void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (gameObject.activeSelf)
            {
                if (_pool.TryGive(out Unit unit))
                {
                    unit.SetMovePoint(_target.position);

                    var unitTransform = unit.transform;
                    unitTransform.SetParent(_container);
                    unitTransform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
                    unitTransform.gameObject.SetActive(true);

                    yield return new WaitForSeconds(unit.ConstructionTime);
                }
                else
                {
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}