using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellUpdateStone : MonoBehaviour
    {
        [SerializeField] private ProcessingCellClick processingCellClick;
        [SerializeField] private Stone stone;
        
        private Color _reserveColor;
        private Vector2Int _idPosition;

        private bool _isLock;
        private bool _isReadyForDestruction;

        public ProcessingCellClick ProcessingCellClick => processingCellClick;
        public Stone Stone => stone;
        
        public Color Color => _reserveColor;

        public Vector2Int IdPosition
        {
            get => _idPosition;
            set => _idPosition = value;
        }

        public bool IsLock => _isLock;
        public bool IsReadyForDestruction => _isReadyForDestruction;

        public void ReserveColor(Color color) => 
            _reserveColor = color;

        public void Select()
        {
            stone.SetColor(Color.black);
        }

        public void Deselect()
        {
            stone.SetColor(_reserveColor);
        }

        public void TakeStone(Vector3 dispensingPosition)
        {
            _isLock = true;
            
            stone.SetColor(_reserveColor);
            stone.StartMovingIntoCell(dispensingPosition, transform.position, StoneReceived);
        }

        public void StoneReceived()
        {
            _isLock = false;
        }

        public void MarkingForDestruction()
        {
            _isLock = true;
            _isReadyForDestruction = true;
        }

        public void DestroyStone()
        {
            stone.SetColor(Color.clear);

            _isReadyForDestruction = false;
        }
    }
}
