using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Dictionary<State, List<KeyValuePair<Transition, State>>> stateTransitions = new Dictionary<State, List<KeyValuePair<Transition, State>>>();

    State currentState;

    public void Update()
    {
        if (currentState == null) return;

        var transitions = stateTransitions[currentState];

        // check state transitions
        foreach (var transition in transitions)
        {
            if (transition.Key.ToTransition())
            {
                SetState(transition.Value);
                break;
            }
        }

        // update state
        currentState.OnUpdate();
    }

    public void SetState(State newState)
    {
        currentState.OnExit();

        newState.OnEnter();
        currentState = newState;
    }
}
