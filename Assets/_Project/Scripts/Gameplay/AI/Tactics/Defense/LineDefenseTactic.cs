using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.AI.Deactevators;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    public class LineDefenseTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _turretSites;
        private readonly List<string> _turretIds;
        private readonly IBuildingSitesController _controller;
        private readonly ILineDefenseTacticSetting _setting;
        private readonly ILineAreaDefenseAI _lineAreaDefenseAI;
        private readonly ITacticTool _tool;
        private readonly Randomizer _randomizer;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private int _score;
        private bool _canAction;
        private bool _isMaxTurrets;

        public LineDefenseTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            ILineDefenseTacticSetting setting,
            IMainAreaDefenseAI mainAreaDefenseAI,
            Randomizer randomizer)
        {
            if (mainAreaDefenseAI == null)
                throw new ArgumentNullException(nameof(mainAreaDefenseAI));

            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
            _lineAreaDefenseAI = mainAreaDefenseAI.GetLineAreaDefenseAI(_setting.LineDefanceNumber);

            _turretSites = GetTurretSites();
            _turretIds = new List<string>();
            _score = _setting.MaxScore;
            _canAction = false;
            _isMaxTurrets = false;

            foreach (var id in setting.GetDefenseBuildingIds())
                _turretIds.Add(id);

            _controller.SitesBuildCompleted += OnBuildedTurret;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public bool IsDeactivated => _tacticDeactivator.IsDeactivate;

        public void Init(ITacticDeactivator tacticDeactivator)
        {
            _tacticDeactivator ??= tacticDeactivator ?? throw new ArgumentNullException(nameof(tacticDeactivator));
        }

        public void CalculateScore()
        {
            if (_isMaxTurrets)
            {
                _score = _setting.MinScore;
                _canAction = false;

                return;
            }

            int addedScore = _setting.ScoreForActorInArea * _lineAreaDefenseAI.GetNumberActorsInArea();
            _score = _setting.MaxScore + addedScore;
            _canAction = TryBuild();
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildedTurret;

            foreach (var turretSite in _turretSites)
                turretSite.ActorMissing -= OnRemoveFactory;
        }

        private List<IRegisteredBuildingSite> GetTurretSites()
        {
            int mainSiteId = _lineAreaDefenseAI.MainSiteId;
            IEnumerable<IRegisteredBuildingSite> sites = _controller.GetNearbyBuildingSiteById(mainSiteId);

            foreach (var site in sites)
                site.ActorMissing += OnRemoveFactory;

            return new List<IRegisteredBuildingSite>(sites);
        }

        private bool TryBuild()
        {
            foreach (var site in _turretSites)
            {
                if (site.HasBuilding || site.IsConstruction)
                    continue;

                if (TryFindProductionOption(site))
                    return true;
            }

            return false;
        }

        private bool TryFindProductionOption(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite.TryGetProductionStorage(out IProductionStorage productionStorage) == false)
                return false;

            int randomIndex = _randomizer.GetRangeZero(_turretIds.Count);
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

            if (_turretSites.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
                CheckTurretSites();
        }

        private void OnBuildedTurret(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            bool isbuildingSiteValid = false;

            foreach (var turretSite in _turretSites)
            {
                if (turretSite.BuildingSiteId == buildingSite.BuildingSiteId)
                {
                    isbuildingSiteValid = true;

                    break;
                }
            }

            if (isbuildingSiteValid == false)
                return;

            CheckTurretSites();
        }

        private void CheckTurretSites()
        {
            foreach (var turretSite in _turretSites)
            {
                if (turretSite.HasBuilding == false && turretSite.IsConstruction == false)
                {
                    _isMaxTurrets = false;

                    return;
                }
            }

            _isMaxTurrets = true;
        }
    }
}