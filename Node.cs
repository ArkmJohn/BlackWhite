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

	void Start()
	{
		currentState = NodeStates.RUNNING;
	}

	//public abstract void reset ();

	// If the state is a success, set the value of currentState to success
	private void SuccessState()
	{
		currentState = NodeStates.SUCCESS;
	}

	// If the state is a failure, set the value of currentState to failure
	private void FailureState()
	{
		currentState = NodeStates.FAILURE;
	}

	// If the state is a running, set the value of currentState to running
	private void RunningState()
	{
		currentState = NodeStates.RUNNING;
	}

	public void isSuccess()
	{
		return this.currentState.Equals (NodeStates.SUCCESS);
	}

	public void isFail()
	{
		return this.currentState.Equals (NodeStates.FAILURE);
	}

	public void isRunning()
	{
		return this.currentState.Equals (NodeStates.RUNNING);
	}

}

