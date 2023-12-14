using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellActivityCheckService : ICellActivityCheckService
    {
        private FieldData _fieldData;

        public void Initialize(FieldData fieldData)
        {
            _fieldData = fieldData;
        }

        public bool IsAllUnlocked()
        {
            if (_fieldData.Cells == null)
            {
                Debug.LogWarning("Cell activity check service is not initialized.");
                return false;
            }

            foreach (CellUpdateStone cell in _fieldData.Cells)
            {
                if (cell.IsLock)
                    return false;
            }

            return true;
        }
    }
}