using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class TargetFinder : MonoBehaviour, ITargetFinder
    {
        [SerializeField] private LayerMask _findedLayerMask;
        [SerializeField][Min(0.1f)] private float _timeUpdate = 0.5f;
        [SerializeField][Range(32, 256)] private int _maxFindedUnits = 64;

        [Header("Debug")]
        [SerializeField] private bool _isDebugEnable;

        private List<ITarget> _targets;
        private IAttackerPresenter _presenter;
        private ITargetFinderConfig _targetConfig;
        private ITeamable _teamable;
        private Transform _transform;
        private Collider[] _foundUnits;
        private WaitForSeconds _tick;

        private void Awake()
        {
            _transform = transform;
            _targets = new List<ITarget>(_maxFindedUnits);
            _tick = new WaitForSeconds(_timeUpdate);
            _foundUnits = new Collider[_maxFindedUnits];
        }

        private void OnEnable()
        {
            _targets.Clear();

            StartCoroutine(Activate());
        }

        private void OnDrawGizmosSelected()
        {
            if (_isDebugEnable == false || gameObject.activeSelf == false || _targetConfig == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _targetConfig.SearchRadius);
        }

        public void Init(IAttackerPresenter presenter, ITargetFinderConfig targetConfig, ITeamable teamable)
        {
            _presenter ??= presenter ?? throw new ArgumentNullException(nameof(presenter));
            _targetConfig ??= targetConfig ?? throw new ArgumentNullException(nameof(targetConfig));
            _teamable ??= teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        private IEnumerator Activate()
        {
            while (gameObject.activeSelf)
            {
                if (_targetConfig == null)
                    yield return null;

                if (TryFindEnemies())
                    _presenter.SetTargets(_targets);


                yield return _tick;
            }
        }

        private bool TryFindEnemies()
        {
            _targets.Clear();

            int count = Physics.OverlapSphereNonAlloc(
                _transform.position,
                _targetConfig.SearchRadius,
                _foundUnits,
                _findedLayerMask,
                QueryTriggerInteraction.Ignore);

            for (int i = 0; i < count; i++)
            {
                Collider collider = _foundUnits[i];

                if (collider.TryGetComponent(out ITarget enemy))
                {
                    if (_teamable.TeamType != enemy.TeamType)
                        _targets.Add(enemy);
                }
            }

            return _targets.Count > 0;
        }
    }
}
