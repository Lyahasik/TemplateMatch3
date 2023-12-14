using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieVsMatch3.Gameplay.Match3
{
    [RequireComponent(typeof(RectTransform))]
    public class Stone : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private MovingIntoTarget movingIntoTarget;

        private RectTransform _rect;

        public RectTransform Rect => _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void SetColor(Color color) => 
            icon.color = color;

        public void StartMovingIntoCell(in Vector3 dispensingPosition, in Vector3 targetPosition, in Action onStop) => 
            movingIntoTarget.StartMoving(dispensingPosition, targetPosition, onStop);
    }
}