using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.ProjectStateMachine.Base
{
    public class StateMachine<TInitializer>
    {
        public StateMachine(params IState<TInitializer>[] states)
        {
            _states = new Dictionary<Type, IState<TInitializer>>(states.Length);

            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }
        }
    
        private IState<TInitializer> _currentState;

        private readonly Dictionary<Type, IState<TInitializer>> _states;
    
        private bool _isTicking;

        public void SwitchState<TState>() where TState : IState<TInitializer>
        {
            TryExitPreviousState<TState>();

            GetNewState<TState>();
        
            TryEnterNewState<TState>();
        
            TryTickNewState<TState>();
        }
        
        public void SwitchState<TState, T0>(T0 arg) where TState : IState<TInitializer>
        {
            TryExitPreviousState<TState>();

            GetNewState<TState>();
        
            TryEnterNewState<TState, T0>(arg);
        
            TryTickNewState<TState>();
        }

        private void TryExitPreviousState<TState>() where TState : IState<TInitializer>
        {
            if (_currentState is IExitable exitable)
            {
                exitable.OnExit();
            }
        }

        private void TryEnterNewState<TState>() where TState : IState<TInitializer>
        {
            if (_currentState is IEnterable enterable)
            {
                enterable.OnEnter();
            }
        }
        
        private void TryEnterNewState<TState, T0>(T0 arg) where TState : IState<TInitializer>
        {
            if (_currentState is IEnterableWithOneArg<T0> enterable)
            {
                enterable.OnEnter(arg);
            }
        }

        private void GetNewState<TState>() where TState : IState<TInitializer>
        {
            var newState = GetState<TState>();
            _currentState = newState;
        }

        private void TryTickNewState<TState>() where TState : IState<TInitializer>
        {
            if (_currentState is ITickable tickable)
            {
                _isTicking = true;
                StartTick(tickable);
            }
            else
            {
                _isTicking = false;
            }
        }

        private async void StartTick(ITickable tickable)
        {
            while (_isTicking)
            {
                tickable.Tick();
                await Task.Yield();
            }
        }

        private TState GetState<TState>() where TState : IState<TInitializer>
        {
            return (TState)_states[typeof(TState)];
        }
    }
}