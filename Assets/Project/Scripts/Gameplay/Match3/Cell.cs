using UnityEngine;
using UnityEngine.UI;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image icon;

        private Color _reserveColor;

        public Color Color => _reserveColor;

        public string GetColorName()
        {
            if (icon.color == Color.blue)
                return "blue";
            if (icon.color == Color.green)
                return "green";
            
            return "red";
        }
        
        public void ReserveColor(Color color) => 
            _reserveColor = color;
        
        public void SetColor()
        {
            icon.color = _reserveColor;
        }
    }
}
