using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(MultiActorSpawnerSourceSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(MultiActorSpawnerSourceSource))]
    public class MultiActorSpawnerSourceSource : ActorSpawnerSource { }
}