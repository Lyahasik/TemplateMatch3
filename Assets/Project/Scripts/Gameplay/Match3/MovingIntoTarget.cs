using System;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class MovingIntoTarget : MonoBehaviour
    {
        private const float MinDistance = 0.001f;
        
        private Vector3 _targetPosition;
        
        [SerializeField] private float speed = 500f;
        private float _step;

        private event Action OnStop;
        
        private void Update()
        {
            StepTowardsTarget();
        }

        private void OnDisable()
        {
            OnStop?.Invoke();
        }

        public void StartMoving(in Vector3 dispensingPosition, in Vector3 targetPosition, in Action onStop)
        {
            transform.position = dispensingPosition;
            _targetPosition = targetPosition;
            OnStop = onStop;

            enabled = true;
        }

        private void StepTowardsTarget()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

            if (Vector3.Distance(transform.position, _targetPosition) <= MinDistance)
                enabled = false;
        }
    }
}