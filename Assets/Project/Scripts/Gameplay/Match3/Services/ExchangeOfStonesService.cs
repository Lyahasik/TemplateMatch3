using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class ExchangeOfStonesService : IExchangeOfStonesService
    {
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        private readonly IStonesDestructionMatch3Service _stonesDestructionMatch3Service;

        private CellUpdateStone _firstCellUpdateStone;

        public ExchangeOfStonesService(IDefiningConnectionsMatch3Service definingConnectionsMatch3Service,
            IStonesDestructionMatch3Service stonesDestructionMatch3Service)
        {
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
            _stonesDestructionMatch3Service = stonesDestructionMatch3Service;
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
                StartSwap(cellUpdateStone);
            }
            else
            {
                Deselect();
                SetFirstCell(cellUpdateStone);
            }
        }

        private void SetFirstCell(CellUpdateStone cellUpdateStone)
        {
            _firstCellUpdateStone = cellUpdateStone;
            _firstCellUpdateStone.Select();
        }

        public void Deselect()
        {
            _firstCellUpdateStone?.Deselect();
            _firstCellUpdateStone = null;
        }

        private void StartSwap(CellUpdateStone secondCellUpdateStone)
        {
            Color oldSecondColor = secondCellUpdateStone.Color;
            
            secondCellUpdateStone.ReserveColor(_firstCellUpdateStone.Color);
            secondCellUpdateStone.TakeStone(_firstCellUpdateStone.transform.position);
            
            _firstCellUpdateStone.ReserveColor(oldSecondColor);
            _firstCellUpdateStone.TakeStone(secondCellUpdateStone.transform.position);

            _firstCellUpdateStone = null;
            _stonesDestructionMatch3Service.TryDestroy();
        }
    }
}