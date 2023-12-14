using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Core.Coroutines;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class StonesDestructionMatch3Service : IStonesDestructionMatch3Service
    {
        private readonly ICellActivityCheckService _cellActivityCheckService;
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        private readonly CoroutinesContainer _coroutinesContainer;

        private bool _isBeingChecked;

        public StonesDestructionMatch3Service(ICellActivityCheckService cellActivityCheckService,
            IDefiningConnectionsMatch3Service definingConnectionsMatch3Service,
            CoroutinesContainer coroutinesContainer)
        {
            _cellActivityCheckService = cellActivityCheckService;
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
            _coroutinesContainer = coroutinesContainer;
        }

        public void TryDestroy()
        {
            if (_isBeingChecked)
                return;

            _isBeingChecked = true;
            _coroutinesContainer.StartCoroutine(WaitingForReadiness());
        }

        private IEnumerator WaitingForReadiness()
        {
            Debug.Log("Waiting");
            while (!_cellActivityCheckService.IsAllUnlocked())
                yield return null;

            TryStartDestroy();
        }

        private void TryStartDestroy()
        {
            List<CellUpdateStone> cells = _definingConnectionsMatch3Service.GetListForms();
            
            if (cells != null)
            {
                foreach (CellUpdateStone cell in cells)
                {
                    cell.DestroyStone();
                }
            }
            else
            {
                Debug.Log("Not start destroy");
            }
            
            _isBeingChecked = false;
        }
    }
}