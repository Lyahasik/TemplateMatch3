using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class SelectionState : IState
    {
        private readonly FieldMatch3ActiveArea _fieldMatch3ActiveArea;

        public SelectionState(FieldMatch3ActiveArea fieldMatch3ActiveArea)
        {
            _fieldMatch3ActiveArea = fieldMatch3ActiveArea;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");

            _fieldMatch3ActiveArea.enabled = true;
        }

        public void Exit()
        {
            _fieldMatch3ActiveArea.enabled = false;
        }
    }
}