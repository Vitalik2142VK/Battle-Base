using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(SingleActorSpawnerSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(SingleActorSpawnerSource))]
    public class SingleActorSpawnerSource : ActorSpawnerSource { }
}