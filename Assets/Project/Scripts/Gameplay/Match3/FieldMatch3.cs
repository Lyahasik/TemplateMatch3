using System.Collections.Generic;
using UnityEngine;
using ZombieVsMatch3.Gameplay.Match3.Services;

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

        public void Initialize(IExchangeOfStonesService exchangeOfStonesService)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                spawns[i].Initialize();
                
                rows[i].Initialize(exchangeOfStonesService, fieldMatch3ActiveArea);
                
                fieldMatch3ActiveArea.Construct(exchangeOfStonesService);
            }
        }
    }
}
