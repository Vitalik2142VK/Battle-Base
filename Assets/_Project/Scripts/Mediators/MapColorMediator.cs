using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Map;
using BattleBase.UI.PopUps;
using UnityEngine;

namespace BattleBase.Mediators
{
    public class MapColorMediator : MediatorBase
    {
        [SerializeField] private ColorSettingsPopUp _colorSettingsPopUp;
        [SerializeField] private List<Territory> _territories;
        [SerializeField] private float _lightenFactor = 0.3f;

        private void OnEnable() =>
            _colorSettingsPopUp.Changed += OnColorChanged;

        private void OnDisable() =>
            _colorSettingsPopUp.Changed -= OnColorChanged;

        public override void Init()
        {
            OnColorChanged();
        }

        private void OnColorChanged()
        {
            Debug.Log("OnColorChanged");

            foreach (Territory territory in _territories)
            {
                if (territory.Owner == TerritoryOwnerType.Enemy)
                    territory.SetColor(_colorSettingsPopUp.EnemyColor);
                else if (territory.Owner == TerritoryOwnerType.Player)
                    territory.SetColor(_colorSettingsPopUp.PlayerColor);
                else
                    territory.SetColor(Color.Lerp(_colorSettingsPopUp.EnemyColor, Color.white, _lightenFactor));
            }
        }
    }
}