using System;
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
            _unitsUpgradeModel.UnitSelectionChanged += UpdateInfo;
            UpdateInfo();
        }

        private void OnDisable() =>
            _unitsUpgradeModel.UnitSelectionChanged -= UpdateInfo;

        private void UpdateInfo()
        {
            GameObject actor = _previewInstanceFactory.Create(_unitsUpgradeModel.Selected.SourcePrefab);

            if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                colorChanger.Change(_teamColorModel.PlayerColor);

            _renderingModelInstaller.SetModel(actor, GetTargetOffset(actor));
            GetTargetOffset(actor);
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