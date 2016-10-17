using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
	// ENUM States
	public enum NodeStates
	{
		SUCCESS,
		FAILURE,
		RUNNING
	}

	// variable that stores the node states
	public NodeStates currentState;

	// boolean values for success, failure and running
	protected bool successState;
	protected bool failureState;
	protected bool runningState;

	void Start()
	{
		// Setting the current state to running
		currentState = NodeStates.RUNNING;
	}

	// For Sequence Node
	public void SequenceNode()
	{
		// If the state is running
		if (currentState == NodeStates.RUNNING) 
		{
			// If the state is a success then change the current state value to 'SUCCESS'
			if (successState) {
				currentState = NodeStates.SUCCESS;
			}
		}
	}

	public void SelectorNode()
	{
		// If the state is running
		if (currentState == NodeStates.RUNNING) 
		{
			// If the state is a success or true, then change the state to 'SUCCESS'
			if (successState) 
			{
				currentState = NodeStates.SUCCESS;
			}

			// If the state is a failure or false, then change the state to 'FAILURE'
			else if(failureState)
			{
				currentState = NodeStates.FAILURE;
			}
		}

	}

	public void InverterNode()
	{
		// IF the current state is running
		if(currentState == NodeStates.RUNNING)
		{
			// If the state is true, then change the state to false
			if (successState) 
			{
				currentState = NodeStates.FAILURE;
			}
				
			// If the state is false, then change the current state value to true.
			else if(failureState)
			{
				currentState = NodeStates.SUCCESS;
			}
		}
	}

	public void Repeater()
	{
		if(successState)
		{
			currentState = NodeStates.RUNNING;
		}

		else if(failureState)
		{
			currentState = NodeStates.RUNNING;
		}
	}
}
