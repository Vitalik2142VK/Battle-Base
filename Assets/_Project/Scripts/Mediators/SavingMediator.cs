using System;
using System.Collections.Generic;
using BattleBase.Abstract;
using BattleBase.SaveService;
using UnityEngine;
using VContainer;

namespace BattleBase.Mediators
{
    public class SavingMediator : MediatorBase, IInjectable
    {
        [SerializeField] private List<MonoBehaviour> _saveables;

        private ISaver _saver;

        [Inject]
        public void Construct(ISaver saver) =>
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));            

        public override void Init() =>
            ProcessSaveables(saveable => saveable.Load(), ignoreNull: false);

        private void OnDisable() =>
            ProcessSaveables(saveable => saveable.Save(), ignoreNull: true);

        private void ProcessSaveables(Action<ISaveable> action, bool ignoreNull)
        {
            string errorMessage = "Element in _saveables list is null";

            foreach (MonoBehaviour mono in _saveables)
            {
                if (mono == null)
                {
                    if (ignoreNull == false)
                        throw new NullReferenceException(errorMessage);

                    Debug.LogWarning(errorMessage);

                    continue;
                }

                if (mono is ISaveable saveable)
                    action(saveable);
                else
                    throw new InvalidOperationException($"Object: {mono.gameObject.name}, Component: {mono.GetType().Name} does not implement ISaveable");
            }

            _saver.Save();
        }
    }
}