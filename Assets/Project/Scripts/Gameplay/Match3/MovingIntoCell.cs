using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class MovingIntoCell : MonoBehaviour
    {
        private Vector3 _targetPosition;
        
        [SerializeField] private float speed = 500f;
        private float _step;
        private void Update()
        {
            StepTowardsTarget();
        }

        public void StartMoving(Vector3 dispensingPosition, Vector3 targetPosition)
        {
            transform.position = dispensingPosition;
            _targetPosition = targetPosition;

            enabled = true;
        }

        private void StepTowardsTarget()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

            if (Vector3.Distance(transform.position, _targetPosition) <= float.MinValue)
                enabled = false;
        }
    }
}