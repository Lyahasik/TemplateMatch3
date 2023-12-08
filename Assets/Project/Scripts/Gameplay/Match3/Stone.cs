using UnityEngine;
using UnityEngine.UI;

namespace ZombieVsMatch3.Gameplay.Match3
{
    [RequireComponent(typeof(RectTransform))]
    public class Stone : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private MovingIntoCell movingIntoCell;

        private RectTransform _rect;

        public RectTransform Rect => _rect;
        
        public Color Color => icon.color;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void SetColor(Color color) => 
            icon.color = color;

        public void StartMovingIntoCell(Vector3 dispensingPosition, Vector3 targetPosition) => 
            movingIntoCell.StartMoving(dispensingPosition, targetPosition);
    }
}