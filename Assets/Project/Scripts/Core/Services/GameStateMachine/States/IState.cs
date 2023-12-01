namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public interface IState : IOutputState
    {
        void Enter();
    }
}