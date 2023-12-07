using System.Collections.Generic;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private List<Cell> cells;
        
        private DistributorStones _distributorStones;

        public List<Cell> Cells => cells;

        public void Construct(DistributorStones distributorStones)
        {
            _distributorStones = distributorStones;
        }
        
        public void Initialize() {}
    }
}
