using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class ColorSettingsPopUp : PopUp, IInjectable
    {
        [SerializeField] private TeamColorPanel _playerColorSet;
        [SerializeField] private TeamColorPanel _enemyColorSet;

        private TeamColorModel _colorModel;

        [Inject]
        public void Construct(TeamColorModel colorModel) =>
            _colorModel = colorModel ?? throw new ArgumentNullException(nameof(colorModel));

        private void Awake()
        {
            IReadOnlyList<Color> colors = _colorModel.Colors;

            _enemyColorSet.Init(colors);
            _playerColorSet.Init(colors);
            UpdateInfo();
        }

        private void OnEnable()
        {
            _playerColorSet.Clicked += OnClickPlayerColor;
            _enemyColorSet.Clicked += OnClickEnemyColor;
            UpdateInfo();
        }

        private void OnDisable()
        {
            _playerColorSet.Clicked -= OnClickPlayerColor;
            _enemyColorSet.Clicked -= OnClickEnemyColor;
        }

        private void OnClickPlayerColor(int index)
        {
            _colorModel.SetPlayerColorIndex(index);
            UpdateInfo();
        }

        private void OnClickEnemyColor(int index)
        {
            _colorModel.SetEnemyColorIndex(index);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            int playerColorIndex = _colorModel.PlayerColorIndex;
            int enemyColorIndex = _colorModel.EnemyColorIndex;

            _enemyColorSet.EnableInteractableAll();
            _enemyColorSet.DisableInteractable(playerColorIndex);
            _enemyColorSet.SelectOnly(enemyColorIndex);

            _playerColorSet.EnableInteractableAll();
            _playerColorSet.DisableInteractable(enemyColorIndex);
            _playerColorSet.SelectOnly(playerColorIndex);
        }
    }
}