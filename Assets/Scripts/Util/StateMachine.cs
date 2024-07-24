using System;
using System.Collections.Generic;

public class StateMachine<T> where T : Enum
{
    private Dictionary<T, BaseState<T>> stateDic = new Dictionary<T, BaseState<T>>();
    private BaseState<T> curState;

    public void Start(T startState)
    {
        curState = stateDic[startState];
        curState.Enter();
    }

    public void Update()
    {
        curState.Update();
        curState.Transition();
    }

    public void LateUpdate()
    {
        curState.LateUpdate();
    }

    public void FixedUpdate()
    {
        curState.FixedUpdate();
    }

    public void AddState(T stateEnum, BaseState<T> state)
    {
        state.SetStateMachine(this);
        stateDic.Add(stateEnum, state);
    }

    public void ChangeState(T stateEnum)
    {
        curState.Exit();
        curState = stateDic[stateEnum];
        curState.Enter();
    }

	public T GetCurrentState()
	{
		foreach (var kvp in stateDic)
		{
			if (kvp.Value == curState)
			{
				return kvp.Key;
			}
		}
		throw new InvalidOperationException("Current state not found in state dictionary.");
	}

	public TState GetState<TState>(T stateType) where TState : BaseState<T>
	{
		if (stateDic.TryGetValue(stateType, out BaseState<T> state))
		{
			return state as TState;
		}
		throw new InvalidOperationException("State not found or invalid type.");
	}
}