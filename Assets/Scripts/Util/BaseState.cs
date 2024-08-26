using System;

public class BaseState<T> where T : Enum
{
	private StateMachine<T> stateMachine;

	public void SetStateMachine(StateMachine<T> stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	protected void ChangeState(T stateEnum)
	{
		stateMachine.ChangeState(stateEnum);
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void Update() { }
	public virtual void LateUpdate() { }
	public virtual void FixedUpdate() { }
	public virtual void Transition() { }
}
