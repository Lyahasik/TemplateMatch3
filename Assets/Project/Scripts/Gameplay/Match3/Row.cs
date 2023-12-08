using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private List<CellUpdateStone> cells;

        public List<CellUpdateStone> Cells => cells;

        public void Initialize(IExchangeOfStonesService exchangeOfStonesService, FieldMatch3ActiveArea fieldMatch3ActiveArea)
        {
            foreach (CellUpdateStone cell in Cells)
            {
                cell.ProcessingCellClick.Construct(exchangeOfStonesService);
                cell.ProcessingCellClick.Subscribe(fieldMatch3ActiveArea);
            }
        }
    }
}
