using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines
{
    public class FiniteStateMachine
    {
        private FiniteState _current;

        // public FiniteStateMachine(FiniteState firstState)
        // {
        //     _current = firstState;
        // }
        
        public void Start(FiniteState startState)
        {
            MoveNextState(startState);
        }

        public void Update()
        {
            _current.Update();
            
            if (_current.TryGetNextState(out FiniteState state) == false)
            {
                return;
            }

            MoveNextState(state);
        }

        private void MoveNextState(FiniteState nextState)
        {
            _current?.Exit();
            _current = nextState;
            _current.Enter();
        }
    }
}