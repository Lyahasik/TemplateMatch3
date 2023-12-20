using System;
using UnityEngine;

using ZombieVsMatch3.Constants;
using ZombieVsMatch3.Gameplay.StaticData;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public class MovingIntoTarget : MonoBehaviour
    {
        private Match3StaticData _match3StaticData;
        
        private Vector3 _targetPosition;

        private float _step;

        private event Action OnStop;

        public void Construct(Match3StaticData match3StaticData)
        {
            _match3StaticData = match3StaticData;
        }
        
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
            float step = _match3StaticData.speedStoneMovement * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

            if (Vector3.Distance(transform.position, _targetPosition) <= ConstantValues.MIN_DISTANCE)
                enabled = false;
        }
    }
}