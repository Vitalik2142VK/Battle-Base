using System;
using BattleBase.Gameplay.Map;
using BattleBase.Gameplay.MiniMap;

namespace BattleBase.Gameplay.Actors.Colored
{
    public class ActorColorService : IActorColorService
    {
        private readonly IEntityTrackerFactory _trackerFactory;
        private readonly TeamColorModel _teamColorModel;

        public ActorColorService(IEntityTrackerFactory trackerFactory, TeamColorModel teamColorModel)
        {
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
            _teamColorModel = teamColorModel ?? throw new ArgumentNullException(nameof(teamColorModel));
        }

        public void EstabilshColor(IActor actor, IActorView view)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            actor.ChangeColor(_teamColorModel.GetColor(actor.TeamType));

            if (view.TryGetViewComponent(out IColoredActorView coloredView))
            {
                PositionTrackingType trackingType;

                if (actor.IsStatic)
                    trackingType = PositionTrackingType.Static;
                else
                    trackingType = PositionTrackingType.PerFrame;

                _trackerFactory.CreateTracker(coloredView.Trackable, trackingType);
            }
        }
    }
}