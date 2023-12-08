using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Extensions;

namespace ZombieVsMatch3.Gameplay.Match3
{
    [RequireComponent(typeof(RectTransform))]
    public class DistributorStones : MonoBehaviour
    {
        private RectTransform _rect;
        
        private Queue<CellUpdateStone> _deliveryQueue;
        private CellUpdateStone _currentCell;
        private RectTransform _rectStone;

        private void Awake() => 
            _rect = GetComponent<RectTransform>();

        private void Update() => 
            GiveOutNextStone();

        public void Initialize() => 
            _deliveryQueue = new Queue<CellUpdateStone>();

        public void CellPuttingInQueueDelivery(CellUpdateStone cellUpdateStone) => 
            _deliveryQueue.Enqueue(cellUpdateStone);

        public void StartDistribute()
        {
            GiveOutStone();
        }

        private void GiveOutNextStone()
        {
            if (_currentCell == null)
                return;

            if (!_rectStone.IsIntersectingRectangles(_rect))
            {
                if (_deliveryQueue.Count == 0)
                {
                    _currentCell = null;
                    return;
                }

                GiveOutStone();
            }
        }

        private void GiveOutStone()
        {
            _currentCell = _deliveryQueue.Dequeue();
            _currentCell.TakeStone(transform.position);
            _rectStone = _currentCell.Stone.Rect;
        }
    }
}