using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellUpdateStone : MonoBehaviour
    {
        [SerializeField] private ProcessingCellClick processingCellClick;
        [SerializeField] private Stone stone;

        private ICellsStateCheckService _cellsStateCheckService;
        
        private Color _currentColor;
        private Vector2Int _idPosition;

        private bool _isLock;
        private bool _isReadyForDestruction;

        public ProcessingCellClick ProcessingCellClick => processingCellClick;
        public Stone Stone => stone;
        
        public Color Color => _currentColor;

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

        public void SetColor(Color color) => 
            _currentColor = color;

        public void Select()
        {
            stone.SetColor(Color.black);
        }

        public void Deselect()
        {
            stone.SetColor(_currentColor);
        }

        public void TakeStone(Vector3 dispensingPosition, in Color color)
        {
            _isLock = true;
            _cellsStateCheckService.AddProcessingCell();

            _currentColor = color;
            stone.SetColor(_currentColor);
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
            _currentColor = Color.clear;
            stone.SetColor(_currentColor);

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
