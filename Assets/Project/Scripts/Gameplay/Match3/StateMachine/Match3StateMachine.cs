using System;
using System.Collections.Generic;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine
{
    public class Match3StateMachine : IMatch3StateMachine
    {
        private Dictionary<Type, IOutputState> _states;
        
        private IOutputState _activeState;
        
        public void Initialize(IFillingCellsMatch3Service fillingCellsMatch3Service,
            FieldMatch3 field,
            FieldMatch3ActiveArea fieldMatch3ActiveArea,
            IExchangeOfStonesService exchangeOfStonesService,
            IStonesDestructionMatch3Service stonesDestructionMatch3Service)
        {
            _states = new Dictionary<Type, IOutputState>
            {
                [typeof(StartFillingState)] = new StartFillingState(fillingCellsMatch3Service, field),
                [typeof(FillingState)] = new FillingState(fillingCellsMatch3Service),
                [typeof(SelectionState)] = new SelectionState(fieldMatch3ActiveArea),
                [typeof(SwappingState)] = new SwappingState(exchangeOfStonesService),
                [typeof(DestructionState)] = new DestructionState(stonesDestructionMatch3Service),
                [typeof(StopState)] = new StopState()
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
        
        private TState GetState<TState>() where TState : class, IOutputState => 
            _states[typeof(TState)] as TState;
    }
}