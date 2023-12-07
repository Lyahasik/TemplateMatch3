using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class FieldMatch3 : MonoBehaviour
    {
        [SerializeField] private List<DistributorStones> spawns;
        [SerializeField] private List<Row> rows;

        public List<DistributorStones> Spawns => spawns;
        public List<Row> Rows => rows;

        public void Initialize(IDefiningConnectionsMatch3Service definingConnectionsMatch3Service)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                spawns[i].Initialize();
                
                rows[i].Construct(spawns[i]);
            }
        }
    }
}
