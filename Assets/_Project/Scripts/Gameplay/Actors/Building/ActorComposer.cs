using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.ComponentImprovement;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public class ActorComposer : IActorComposer
    {
        private readonly IActorCreator _actorCreator;
        private readonly IActorsController _actorsController;
        private readonly IActorUpgrader _actorUpgrader;
        private readonly IActorColorService _colorService;

        public ActorComposer(
            IActorCreator actorCreator, 
            IActorsController actorsController,
            IActorUpgrader actorUpgrader,
            IActorColorService colorService)
        {
            _actorCreator = actorCreator ?? throw new ArgumentNullException(nameof(actorCreator));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            _actorUpgrader = actorUpgrader ?? throw new ArgumentNullException(nameof(actorUpgrader));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
        }

        public Actor Compose(ActorView view, IActorConfig config, TeamType team)
        {
            Actor actor = _actorCreator.Create(view, config);

            _actorUpgrader.UpgradeActorComponents(team, actor);
            _actorsController.AddActor(actor);

            actor.SetTeam(team);
            actor.Enable();

            _colorService.EstabilshColor(actor, view);

            return actor;
        }
    }
}