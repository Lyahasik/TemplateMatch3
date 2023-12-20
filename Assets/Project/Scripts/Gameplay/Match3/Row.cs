using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.StaticData;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private List<CellUpdateStone> cells;

        public List<CellUpdateStone> Cells => cells;

        public void Initialize(IExchangeOfStonesService exchangeOfStonesService, 
            FieldMatch3ActiveArea fieldMatch3ActiveArea,
            ICellsStateCheckService cellsStateCheckService,
            Match3StaticData match3StaticData)
        {
            foreach (CellUpdateStone cell in Cells)
            {
                cell.Construct(cellsStateCheckService);
                cell.Initialize(match3StaticData);
                cell.ProcessingCellClick.Construct(exchangeOfStonesService);
                cell.ProcessingCellClick.Subscribe(fieldMatch3ActiveArea);
            }
        }
    }
}
