using System;

using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface ICellsStateCheckService : IService
    {
        public void Initialize(FieldData fieldData);

        public void AddProcessingCell();
        public void RemoveProcessingCell();
        public void ExecuteAfterProcessing(in Action method);
        
        public bool IsAllNoEmpty();
        public bool IsAllUnlocked();
    }
}