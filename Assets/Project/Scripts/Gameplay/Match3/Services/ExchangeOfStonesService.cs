using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class ExchangeOfStonesService : IExchangeOfStonesService
    {
        private readonly IMatch3StateMachine _match3StateMachine;
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        private readonly ICellsStateCheckService _cellsStateCheckService;

        private CellUpdateStone _firstCellUpdateStone;
        private CellUpdateStone _secondCellUpdateStone;
        private bool _isPrepared;

        public ExchangeOfStonesService(IMatch3StateMachine match3StateMachine, 
            IDefiningConnectionsMatch3Service definingConnectionsMatch3Service,
            ICellsStateCheckService cellsStateCheckService)
        {
            _match3StateMachine = match3StateMachine;
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
            _cellsStateCheckService = cellsStateCheckService;
        }

        public void HitCell(CellUpdateStone cellUpdateStone)
        {
            if (_firstCellUpdateStone == null)
            {
                SetFirstCell(cellUpdateStone);
            }
            else if (_definingConnectionsMatch3Service.IsNeighboringCells(_firstCellUpdateStone.IdPosition,
                         cellUpdateStone.IdPosition))
            {
                _secondCellUpdateStone = cellUpdateStone;
                _match3StateMachine.Enter<SwappingState>();
            }
            else
            {
                Deselect();
                SetFirstCell(cellUpdateStone);
            }
        }

        public void StartSwap(bool isBack = false)
        {
            PrepareSwap();
            
            Color oldSecondColor = _secondCellUpdateStone.Color;
            
            _secondCellUpdateStone.TakeStone(_firstCellUpdateStone.transform.position, _firstCellUpdateStone.Color);
            _firstCellUpdateStone.TakeStone(_secondCellUpdateStone.transform.position, oldSecondColor);

            if (isBack)
                Reset();
        }

        private void PrepareSwap()
        {
            if (_isPrepared)
                return;
            
            Debug.Log($"add {nameof(TryStartDestroyed)}");
            _cellsStateCheckService.ExecuteAfterProcessing(TryStartDestroyed);
            _isPrepared = true;
        }

        public void Deselect()
        {
            _firstCellUpdateStone?.Deselect();
            Reset();
        }

        private void TryStartDestroyed()
        {
            Debug.Log("try start destroy");
            if (_definingConnectionsMatch3Service.IsFormAssembled(_firstCellUpdateStone.IdPosition,
                    _firstCellUpdateStone.Color)
                || _definingConnectionsMatch3Service.IsFormAssembled(_secondCellUpdateStone.IdPosition,
                    _secondCellUpdateStone.Color))
            {
                Reset();
                _match3StateMachine.Enter<DestructionState>();
            }
            else
            {
                
                Debug.Log($"add {nameof(EnableSelection)}");
                _cellsStateCheckService.ExecuteAfterProcessing(EnableSelection);
                StartSwap(true);
            }
        }

        private void EnableSelection()
        {
            Debug.Log("start selection state");
            _match3StateMachine.Enter<SelectionState>();
        }

        private void SetFirstCell(CellUpdateStone cellUpdateStone)
        {
            _firstCellUpdateStone = cellUpdateStone;
            _firstCellUpdateStone.Select();
        }

        private void Reset()
        {
            _firstCellUpdateStone = null;
            _secondCellUpdateStone = null;

            _isPrepared = false;
        }
    }
}