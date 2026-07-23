using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialRegistry : IAdvancedMaterialRegistry
    {
        private readonly Dictionary<TeamType, Material> _materials;

        public MaterialRegistry(IMaterialConfig materialConfig)
        {
            _materials = new Dictionary<TeamType, Material> 
            {
                { TeamType.Player, new Material(materialConfig) },
                { TeamType.Enemy, new Material(materialConfig) }
            };
        }

        public void AddMaterials(TeamType team, int materials) =>
            _materials[team].AddMaterials(materials);

        public bool TrySpend(TeamType team, int materials) =>
            _materials[team].TrySpend(materials);

        public IMaterialData GetMaterialData(TeamType team) =>
            _materials[team];

        public bool TryGetTransaction(TeamType team, int materials, out MatetialTransaction matetialTransaction)
        {
            matetialTransaction = null;
            Material material = _materials[team];

            if (material.TrySpend(materials) == false)
                return false;

            matetialTransaction = new MatetialTransaction(material, materials);

            return true;
        }
    }
}