using System.Collections.Generic;
using BattleBase.Utils;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryElevator : MonoBehaviour
    {
        [SerializeField] private TerritorySelector _selector;
        [SerializeField] private Vector3 _distance;
        [SerializeField] private TerritoryPositionAnimationConfig _animationConfig;
        [SerializeField] private TerritoryScaleAnimationConfig _scaleAnimationConfig;

        private readonly Dictionary<Territory, Vector3> _originalPositions = new();
        private readonly Dictionary<Territory, Vector3> _originalScales = new();

        private void OnEnable()
        {
            _selector.Selected += OnTerritorySelected;
            _selector.Unselected += OnTerritoryUnselected;
        }

        private void OnDisable()
        {
            _selector.Selected -= OnTerritorySelected;
            _selector.Unselected -= OnTerritoryUnselected;
        }

        private void OnTerritorySelected(Territory territory)
        {
            Transform territoryTransform = territory.transform;

            if (_originalPositions.ContainsKey(territory) == false)
                _originalPositions[territory] = territoryTransform.localPosition;

            if (_originalScales.ContainsKey(territory) == false)
                _originalScales[territory] = territoryTransform.localScale;

            Vector3 originalPos = _originalPositions[territory];
            Vector3 targetPos = originalPos + _distance;
            Vector3 targetScale = _scaleAnimationConfig != null ? _scaleAnimationConfig.TargetScale : Vector3.one;
            KillTweens(territoryTransform);
            territoryTransform.PlayLocalMove(targetPos, _animationConfig);
            territoryTransform.PlayScale(targetScale, _scaleAnimationConfig);
        }

        private void OnTerritoryUnselected(Territory territory)
        {
            if (_originalPositions.TryGetValue(territory, out Vector3 originalPos) == false)
                return;

            if (_originalScales.TryGetValue(territory, out Vector3 originalScale) == false)
                return;

            Transform territoryTransform = territory.transform;
            KillTweens(territoryTransform);
            territoryTransform.PlayLocalMove(originalPos, _animationConfig);
            territoryTransform.PlayScale(originalScale, _scaleAnimationConfig);
        }

        private void KillTweens(Transform target)
        {
            if (target == null)
                return;

            string moveId = $"LocalMove_{target.gameObject.GetInstanceID()}";
            string scaleId = $"Scale_{target.gameObject.GetInstanceID()}";
            DOTween.Kill(moveId);
            DOTween.Kill(scaleId);
        }
    }
}