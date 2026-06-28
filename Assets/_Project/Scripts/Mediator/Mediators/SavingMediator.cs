using System;
using System.Collections.Generic;
using BattleBase.AudioService;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using BattleBase.SaveService;
using BattleBase.ShopSystem;
using VContainer;

namespace BattleBase.Mediators
{
    public class SavingMediator : MediatorBase, IInjectable
    {
        private readonly List<ISaveable> _saveblesNew = new();

        private ISaver _saver;

        [Inject]
        public void Construct(
            ISaver saver,
            CreditsModel credits,
            AudioVolumeModel volumeModel,
            UnitsUpgradeModel unitsUpgradeModel,
            TeamColorModel teamColorModel,
            TerritoriesModel territoriesModel)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

            _saveblesNew.AddRange(new ISaveable[]
            {
                credits,
                volumeModel,
                unitsUpgradeModel,
                teamColorModel,
                territoriesModel,
            });
        }

        private void OnEnable()
        {
            _saver.ProgressReseted += Load;
        }

        private void OnDisable()
        {
            _saver.ProgressReseted -= Load;

            Save();
            _saver.SaveProgress();
        }

        private void Load()
        {
            foreach (ISaveable saveable in _saveblesNew)
                saveable.Load();
        }

        private void Save()
        {
            foreach (ISaveable saveable in _saveblesNew)
                saveable.Save();
        }
    }
}