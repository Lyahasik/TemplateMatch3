using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.StaticData;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class FieldMatch3 : MonoBehaviour
    {
        [SerializeField] private List<DistributorStones> spawns;
        [SerializeField] private List<Row> rows;

        [Space]
        [SerializeField] private FieldMatch3ActiveArea fieldMatch3ActiveArea;

        public List<DistributorStones> Spawns => spawns;
        public List<Row> Rows => rows;

        public FieldMatch3ActiveArea FieldMatch3ActiveArea => fieldMatch3ActiveArea;

        public void Initialize(IExchangeOfStonesService exchangeOfStonesService,
            ICellsStateCheckService cellsStateCheckService,
            Match3StaticData match3StaticData)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                spawns[i].Initialize();
                
                rows[i].Initialize(exchangeOfStonesService, fieldMatch3ActiveArea, cellsStateCheckService, match3StaticData);
                
                fieldMatch3ActiveArea.Construct(exchangeOfStonesService, cellsStateCheckService);
            }
        }
    }
}
