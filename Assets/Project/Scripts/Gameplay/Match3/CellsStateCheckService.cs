using System;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellsStateCheckService : ICellsStateCheckService
    {
        private int _numberProcessingCells;
 
        private FieldData _fieldData;

        private event Action OnExecuteAfterProcessing;

        public void Initialize(FieldData fieldData)
        {
            _fieldData = fieldData;
        }

        public void AddProcessingCell()
        {
            _numberProcessingCells++;
        }

        public void RemoveProcessingCell()
        {
            if (_numberProcessingCells == 0)
                return;
            
            _numberProcessingCells--;

            if (_numberProcessingCells == 0)
            {
                Action temporary = OnExecuteAfterProcessing;
                OnExecuteAfterProcessing = null;
                temporary?.Invoke();
            }
        }

        public void ExecuteAfterProcessing(in Action method)
        {
            OnExecuteAfterProcessing += method;
        }

        public bool IsAllNoEmpty()
        {
            if (!IsFieldDataInit())
                return false;

            foreach (CellUpdateStone cell in _fieldData.Cells)
            {
                if (cell.Color == Color.clear)
                    return false;
            }

            return true;
        }

        public bool IsAllUnlocked()
        {
            if (!IsFieldDataInit())
                return false;

            foreach (CellUpdateStone cell in _fieldData.Cells)
            {
                if (cell.IsLock)
                    return false;
            }

            return true;
        }

        private bool IsFieldDataInit()
        {
            if (_fieldData.Cells == null)
            {
                Debug.LogWarning("Cell activity check service is not initialized.");
                return false;
            }

            return true;
        }
    }
}