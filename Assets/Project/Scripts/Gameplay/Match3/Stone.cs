using UnityEngine;
using UnityEngine.UI;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class Stone : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private MovingIntoCell movingIntoCell;
        
        public Color Color => icon.color;

        public void SetColor(Color color) => 
            icon.color = color;

        public void StartMovingIntoCell(Vector3 dispensingPosition, Vector3 targetPosition) => 
            movingIntoCell.StartMoving(dispensingPosition, targetPosition);
    }
}