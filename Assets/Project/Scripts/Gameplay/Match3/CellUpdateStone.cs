using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.Stones;
using ZombieVsMatch3.Gameplay.StaticData;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellUpdateStone : MonoBehaviour
    {
        [SerializeField] private ProcessingCellClick processingCellClick;
        [SerializeField] private Stone stone;
        [SerializeField] private Image selectionImage;

        private ICellsStateCheckService _cellsStateCheckService;
        
        private StoneData _currentStoneData;
        private Vector2Int _idPosition;

        private bool _isLock;
        private bool _isReadyForDestruction;

        public ProcessingCellClick ProcessingCellClick => processingCellClick;
        public Stone Stone => stone;
        
        public StoneData StoneData => _currentStoneData;

        public Vector2Int IdPosition
        {
            get => _idPosition;
            set => _idPosition = value;
        }

        public bool IsLock => _isLock;
        public bool IsReadyForDestruction => _isReadyForDestruction;

        public void Construct(ICellsStateCheckService cellsStateCheckService)
        {
            _cellsStateCheckService = cellsStateCheckService;
        }

        public void Initialize(Match3StaticData match3StaticData) => 
            stone.Initialize(match3StaticData);

        public void SetStoneData(in StoneData stoneData)
        {
            Deselect();
            _currentStoneData = stoneData;
        }

        public void Select() => 
            selectionImage.enabled = true;

        public void Deselect() => 
            selectionImage.enabled = false;

        public void TakeStone(Vector3 dispensingPosition, in StoneData stoneData)
        {
            _isLock = true;
            _cellsStateCheckService.AddProcessingCell();
            
            Deselect();
            _currentStoneData = stoneData;
            stone.SetSprite(_currentStoneData.sprite);
            stone.StartMovingIntoCell(dispensingPosition, transform.position, StoneReceived);
        }

        public void MarkingForDestruction()
        {
            _isLock = true;
            _isReadyForDestruction = true;
            _cellsStateCheckService.AddProcessingCell();
        }

        public void DestroyStone()
        {
            _currentStoneData = StoneData.Empty;
            stone.Destroy();

            _isLock = false;
            _isReadyForDestruction = false;
            
            _cellsStateCheckService.RemoveProcessingCell();
        }

        private void StoneReceived()
        {
            _isLock = false;
            _cellsStateCheckService.RemoveProcessingCell();
        }
    }
}
