using UnityEngine;
using System.Collections;

public abstract class Node
{
	// ENUM States
	public enum NodeStates
	{
		SUCCESS,
		FAILURE,
		RUNNING
	}

	// Variable that stores the node states
	protected NodeStates currentState;

	public virtual void Start()
	{
		currentState = NodeStates.RUNNING;
	}

	public abstract void Add (Node node);

	public abstract void reset();

	public abstract void act(SpawnEnemies enemy);

	private static bool isSpecial;

	public virtual bool IsSpecial { 
		get 
		{
			return isSpecial;
		} 
		set 
		{
			isSpecial = value;
		}
	}

	// If the state is a success, set the value of currentState to success
	protected void SuccessState()
	{
		currentState = NodeStates.SUCCESS;
	}

	// If the state is a failure, set the value of currentState to failure
	protected void FailureState()
	{
		currentState = NodeStates.FAILURE;
	}

	// If the state is a running, set the value of currentState to running
	protected void RunningState()
	{
		currentState = NodeStates.RUNNING;
	}

	public bool isSuccess()
	{
		return currentState.Equals (NodeStates.SUCCESS);
	}

	public bool isFail()
	{
		 return currentState.Equals (NodeStates.FAILURE);
	}

	public bool isRunning()
	{
		 return currentState.Equals (NodeStates.RUNNING);
	}

}


