using System.Collections.Generic;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class DistributorStones : MonoBehaviour
    {
        private Queue<Cell> _deliveryQueue;

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
            foreach (Cell cell in _deliveryQueue)
                cell.TakeStone(transform.position);
        }
    }
}