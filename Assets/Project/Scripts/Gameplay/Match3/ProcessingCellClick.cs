using UnityEngine;

using ZombieVsMatch3.Extensions;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    [RequireComponent(typeof(RectTransform))]
    public class ProcessingCellClick : MonoBehaviour
    {
        [SerializeField] private CellUpdateStone cellUpdateStone;

        private IExchangeOfStonesService _exchangeOfStonesService;

        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void Construct(IExchangeOfStonesService exchangeOfStonesService)
        {
            _exchangeOfStonesService = exchangeOfStonesService;
        }

        public void Subscribe(FieldMatch3ActiveArea fieldMatch3ActiveArea)
        {
            fieldMatch3ActiveArea.OnDown += Hit;
        }
        
        private void Hit(Vector3 position)
        {
            if (!_rect.IsDotInside(position))
                return;
            
            _exchangeOfStonesService.HitCell(cellUpdateStone);
        }
    }
}