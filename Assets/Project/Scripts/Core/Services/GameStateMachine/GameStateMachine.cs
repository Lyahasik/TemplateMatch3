using System;
using System.Collections.Generic;

using ZombieVsMatch3.Core.Initialize;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Core.Services.Progress;

namespace ZombieVsMatch3.Core.Services.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly GameData _gameData;
        
        private Dictionary<Type, IOutputState> _states;
        
        private IOutputState _activeState;

        public GameStateMachine(GameData gameData)
        {
            _gameData = gameData;
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IOutputState>
            {
                [typeof(LoadProgressState)] = new LoadProgressState(_gameData.ServicesContainer.Single<IProgressProviderService>()),
                [typeof(LoadSceneState)] = new LoadSceneState(
                    _gameData.CoroutinesContainer,
                    _gameData.Curtain),
                [typeof(MainMenuState)] = new MainMenuState(),
                [typeof(GameplayState)] = new GameplayState()
            };
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IOutputState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        public void Enter<TState, TData>(TData data) where TState : class, IDataState<TData>
        {
            TState state = ChangeState<TState>();
            state.Enter(data);
        }

        private TState GetState<TState>() where TState : class, IOutputState => 
            _states[typeof(TState)] as TState;
    }
}