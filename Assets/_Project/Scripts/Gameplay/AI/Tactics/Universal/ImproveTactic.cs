using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.AI.Deactevators;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public class ImproveTactic : ITactic, IDisposable
    {
        private readonly List<ImproveActorData> _improvableActors;
        private readonly List<string> _improvedActorsIds;
        private readonly IBuildingSitesController _controller;
        private readonly IImproveTacticSetting _setting;
        private readonly ITacticTool _tool;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private ImproveActorData _currentImproveData;
        private int _score;
        private bool _canAction;

        public ImproveTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IImproveTacticSetting setting)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            _improvableActors = new List<ImproveActorData>();
            _improvedActorsIds = new List<string>(_setting.ImprovedActorsIds);
            _score = _setting.MaxScore;
            _canAction = false;

            _controller.SitesBuildCompleted += OnBuildActor;
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
            if (_improvableActors.Count <= 0)
            {
                _score = _setting.MinScore;
                _canAction = false;

                return;
            }

            if (TryImprove())
            {
                _canAction = true;
                _score = _setting.MaxScore;
            }
            else
            {
                _canAction = false;
                _score = _setting.MinScore;
            }
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;
            _currentImproveData.IncreaseLevel();
            _currentImproveData = null;

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildActor;

            foreach (var improveData in _improvableActors)
                improveData.BuildingSite.ActorMissing -= OnRemoveActor;

            _improvableActors.Clear();
        }

        private bool TryImprove()
        {
            if (_improvableActors.Count == 0)
                return false;

            ImproveActorData improveData;

            for (int i = 0; i < _improvableActors.Count; i++)
            {
                improveData = _improvableActors[i];

                if (improveData.ImproveLevel >= _setting.MaxLevelImproved)
                {
                    RemoveActor(i--);

                    continue;
                }

                if (improveData.BuildingSite.TryGetProductionStorage(out IProductionStorage productionStorage))
                {
                    if (_tool.TryFindImproveProduction(productionStorage, out _currentProductionOption))
                    {
                        _currentImproveData = improveData;

                        return true;
                    }
                }
            }

            return false;
        }

        private void RemoveActor(int index)
        {
            int lastIndex = _improvableActors.Count - 1;
            ImproveActorData removedImproveData = _improvableActors[index];
            _improvableActors[index] = _improvableActors[lastIndex];
            _improvableActors.RemoveAt(lastIndex);
            removedImproveData.BuildingSite.ActorMissing -= OnRemoveActor;
        }

        private void OnRemoveActor(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            for (int i = 0; i < _improvableActors.Count; i++)
            {
                ImproveActorData improveData = _improvableActors[i];

                if (improveData.BuildingSite != buildingSite)
                    continue;

                if (buildingSite.HasBuilding == false)
                {
                    RemoveActor(i);

                    return;
                }
            }
        }

        private void OnBuildActor(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            foreach (var factoryId in _improvedActorsIds)
            {
                if (buildingSite.CurrentActorId == factoryId)
                {
                    ImproveActorData improveData = new(buildingSite);

                    _improvableActors.Add(improveData);
                    buildingSite.ActorMissing += OnRemoveActor;

                    return;
                }
            }
        }
    }
}