using System.Collections;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [RequireComponent(typeof(Unit))]
    public class EnemyDetectionSystem : MonoBehaviour
    {
        [SerializeField] private LayerMask _findedLayerMask;
        [SerializeField][Min(5f)] private float _findRadius = 20;
        [SerializeField][Min(0.1f)] private float _timeUpdate = 0.5f;
        [SerializeField][Range(5, 15)] private int _maxFindedUnits = 10;

        [Header("Debug")]
        [SerializeField] private bool _isDebugEnable;

        private Transform _transform;
        private Unit _unit;
        private Collider[] _foundUnits;
        private Coroutine _coroutine;
        private WaitForSeconds _tick;

        private void Awake()
        {
            _transform = transform;
            _unit = GetComponent<Unit>();
            _tick = new WaitForSeconds(_timeUpdate);
            _foundUnits = new Collider[_maxFindedUnits];
        }

        private void OnEnable()
        {
            _coroutine = StartCoroutine(Activate());
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }

        private void OnDrawGizmosSelected()
        {
            if (_isDebugEnable == false)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _findRadius);
        }

        private IEnumerator Activate()
        {
            while (gameObject.activeSelf)
            {
                if (_unit.IsAttacking == false)
                {
                    if (TryFindEnemyUnit(out IUnit unit))
                        _unit.AttackUnit(unit);
                }

                yield return _tick;
            }
        }

        private bool TryFindEnemyUnit(out IUnit unit)
        {
            unit = null;

            int count = Physics.OverlapSphereNonAlloc(
                _transform.position,
                _findRadius,
                _foundUnits,
                _findedLayerMask,
                QueryTriggerInteraction.Ignore);

            for (int i = 0; i < count; i++)
            {
                Collider collider = _foundUnits[i];

                if (collider.TryGetComponent(out unit))
                {
                    if (_unit.SideUnit != unit.SideUnit)
                        return true;
                }
            }

            return false;
        }
    }
}
