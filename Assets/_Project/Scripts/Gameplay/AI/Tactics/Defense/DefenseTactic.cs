using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    public class DefenseTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _turrets;
        private readonly List<string> _turretIds;
        private readonly IBuildingSitesController _controller;
        private readonly IDefenseTacticSetting _setting;
        private readonly ITacticTool _tool;
        private readonly Random _random;

        private IProductionOption _currentProductionOption;
        private int _score;
        private int _numberUnderConstruction;
        private bool _canAction;

        public DefenseTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IDefenseTacticSetting setting)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            UnityEngine.Debug.Log($"DefenseTactic");

            _turrets = new List<IRegisteredBuildingSite>();
            _turretIds = new List<string>();
            _random = new Random();
            _score = _setting.MaxScore;
            _numberUnderConstruction = 0;
            _canAction = false;

            foreach (var id in setting.DefenseBuildingIds)
                _turretIds.Add(id);

            _controller.SitesBuildCompleted += OnBuildedTurret;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public void CalculateScore()
        {
            if (_controller.HasFreeSites == false)
            {
                _score = 0;

                return;
            }

            if (TryBuild())
                _canAction = true;
            else
                _canAction = false;
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;
            _numberUnderConstruction++;

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildedTurret;

            foreach (var factory in _turrets)
                factory.ActorMissing -= OnRemoveFactory;

            _turrets.Clear();
        }

        private bool TryBuild()
        {
            if (_turretIds.Count + _numberUnderConstruction >= _controller.NumberSites)
                return false;

            foreach (var lineNumber in _setting.LineNumbersForBuild)
            {
                if (_controller.TryGetRandomFreeSiteInLine(lineNumber, out IRegisteredBuildingSite site))
                {
                    if (TryFindProductionOption(site))
                        return true;
                }
            }

            return false;
        }

        private bool TryFindProductionOption(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite.TryGetProductionStorage(out IProductionStorage productionStorage) == false)
                return false;


            int randomIndex = _random.Next(_turretIds.Count);
            string randomTurretId = _turretIds[randomIndex];

            return _tool.TryFindSpawnProduction(
                productionStorage,
                randomTurretId,
                out _currentProductionOption);
        }

        private void OnRemoveFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_turrets.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveFactory;
                _turrets.Remove(buildingSite);

                _score += _setting.ScoreForBuild;

                if (_score > _setting.MaxScore)
                    _score = _setting.MaxScore;
            }
        }

        private void OnBuildedTurret(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            bool isbuildingSiteValid = false;

            foreach (var id in _setting.DefenseBuildingIds)
            {
                if (buildingSite.CurrentId == id)
                {
                    isbuildingSiteValid = true;

                    break;
                }
            }

            if (isbuildingSiteValid == false)
                return;

            _score -= _setting.ScoreForBuild;
            _numberUnderConstruction--;

            if (_score < _setting.MinScore)
                _score = _setting.MinScore;

            if (_numberUnderConstruction < 0)
                _numberUnderConstruction = 0;

            _turrets.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveFactory;
        }
    }
}