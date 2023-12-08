using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class CellUpdateStone : MonoBehaviour
    {
        [SerializeField] private ProcessingCellClick processingCellClick;
        [SerializeField] private Stone stone;
        
        private Color _reserveColor;
        private Vector2Int _idPosition;

        public ProcessingCellClick ProcessingCellClick => processingCellClick;
        public Stone Stone => stone;
        
        public Color Color => _reserveColor;

        public Vector2Int IdPosition
        {
            get => _idPosition;
            set => _idPosition = value;
        }

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
            stone.SetColor(_reserveColor);
            stone.StartMovingIntoCell(dispensingPosition, transform.position);
        }
    }
}
