using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Stone stone;

        private Color _reserveColor;
        
        public Color Color => _reserveColor;
        
        public void ReserveColor(Color color) => 
            _reserveColor = color;

        public void TakeStone(Vector3 dispensingPosition)
        {
            stone.SetColor(_reserveColor);
            stone.StartMovingIntoCell(dispensingPosition, transform.position);
        }
    }
}
