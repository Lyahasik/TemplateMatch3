using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine
{
    public interface IMatch3StateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
    }
}