using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class DistributorStones : MonoBehaviour
    {
        private IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;

        private Queue<Cell> _deliveryQueue;

        public void Construct(IDefiningConnectionsMatch3Service definingConnectionsMatch3Service)
        {
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
        }

        public void Initialize()
        {
            _deliveryQueue = new Queue<Cell>();
        }

        public void CellPuttingInQueueDelivery(Cell cell)
        {
            _deliveryQueue.Enqueue(cell);
        }

        public void DistributeStones()
        {
            foreach (Cell generateCellData in _deliveryQueue)
            {
                generateCellData.SetColor();
            }
        }
    }
}