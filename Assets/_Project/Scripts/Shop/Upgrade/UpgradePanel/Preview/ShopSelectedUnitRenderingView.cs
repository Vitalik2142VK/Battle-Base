using System;
using System.Collections;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Map;
using BattleBase.PreviewCreatingSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopSelectedUnitRenderingView : MonoBehaviour, IInjectable
    {
        [SerializeField] private RenderingModelInstaller _renderingModelInstaller;

        private ActorsUpgradeModel _unitsUpgradeModel;
        private TeamColorModel _teamColorModel;
        private IPreviewInstanceFactory _previewInstanceFactory;
        private string _currentRenderedId;
        private Coroutine _renderCoroutine;

        [Inject]
        public void Construct(
            ActorsUpgradeModel unitsUpgradeModel,
            TeamColorModel colorModel,
            IPreviewInstanceFactory previewInstanceFactory)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _teamColorModel = colorModel ?? throw new ArgumentNullException(nameof(colorModel));
            _previewInstanceFactory = previewInstanceFactory ?? throw new ArgumentNullException(nameof(previewInstanceFactory));
        }

        private void OnEnable()
        {
            _unitsUpgradeModel.UnitSelectionChanged += OnUnitSelectionChanged;
            ScheduleRender();
        }

        private void OnDisable()
        {
            _unitsUpgradeModel.UnitSelectionChanged -= OnUnitSelectionChanged;

            if (_renderCoroutine != null)
            {
                StopCoroutine(_renderCoroutine);
                _renderCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            if (_renderingModelInstaller != null)
                _renderingModelInstaller.Clear();
        }

        private void OnUnitSelectionChanged() =>
            ScheduleRender();

        private void ScheduleRender()
        {
            if (isActiveAndEnabled == false)
                return;

            if (_renderCoroutine != null)
                StopCoroutine(_renderCoroutine);

            _renderCoroutine = StartCoroutine(RenderDelayed());
        }

        private IEnumerator RenderDelayed()
        {
            yield return null;

            _renderCoroutine = null;

            if (isActiveAndEnabled == false)
                yield break;

            Render();
        }

        private void Render()
        {
            IActorItemConfig selected = _unitsUpgradeModel.Selected;

            if (selected == null || selected.SourcePrefab == null)
            {
                _renderingModelInstaller.Clear();
                _currentRenderedId = null;

                return;
            }

            if (_currentRenderedId == selected.Id && _renderingModelInstaller.HasModel)
                return;

            GameObject actor = _previewInstanceFactory.Create(selected.SourcePrefab);

            if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                colorChanger.Change(_teamColorModel.PlayerColor);

            Vector3 offset = GetTargetOffset(actor);
            _renderingModelInstaller.SetModel(actor, offset);
            _currentRenderedId = selected.Id;
        }

        private static Vector3 GetTargetOffset(GameObject actor)
        {
            Target target = actor.GetComponentInChildren<Target>(true);

            if (target == null)
                return Vector3.zero;

            return actor.transform.position - target.Position;
        }
    }
}