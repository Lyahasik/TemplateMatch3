using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class MovingIntoCell : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        
        [SerializeField] private float _speed = 1f;
        private float _timeCount;
        private void Update()
        {
            StepTowardsTarget();
        }

        public void StartMoving(Vector3 dispensingPosition, Vector3 targetPosition)
        {
            _startPosition = transform.position;
            _startPosition.y += dispensingPosition.y;
            
            transform.position = _startPosition;
            _targetPosition = targetPosition;

            _timeCount = 0f;
            enabled = true;
        }

        private void StepTowardsTarget()
        {
            _timeCount += _speed * Time.deltaTime;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeCount);
            
            if (_timeCount >= 1f)
                enabled = false;
        }
    }
}