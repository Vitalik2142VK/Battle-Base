using BattleBase.Gameplay.MiniMap;
using System;

namespace BattleBase.Gameplay.Actors.Colored
{
    public class ActorColorService : IActorColorService
    {
        private readonly IEntityTrackerFactory _trackerFactory;
        private readonly IColorGetter _colorGetter;

        public ActorColorService(IEntityTrackerFactory trackerFactory, IColorGetter colorGetter)
        {
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
            _colorGetter = colorGetter ?? throw new ArgumentNullException(nameof(colorGetter));
        }

        public void EstabilshColor(IActor actor, IActorView view)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            actor.ChangeColor(_colorGetter.GetTeamColor(actor.TeamType));

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