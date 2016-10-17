using UnityEngine;
using System.Collections;

public class NodeScript : MonoBehaviour 
{

	public enum NodeStates
	{
		SUCCESS,
		FAILURE,
		RUNNING
	}

	public NodeStates currentState;
	protected bool successState;
	protected bool failureState;
	protected bool runningState;

	void Start()
	{
		// Setting the current state to running
		currentState = NodeStates.RUNNING;
	}

	public void SequenceNode()
	{
		if (currentState == NodeStates.RUNNING) 
		{
			if (successState) {
				currentState = NodeStates.SUCCESS;
			}
		}
	}

	public void SelectorNode()
	{
		if (currentState == NodeStates.RUNNING) 
		{
			if (successState) 
			{
				currentState = NodeStates.SUCCESS;
			}

			else if(failureState)
			{
				currentState = NodeStates.FAILURE;
			}
		}

	}

	public void InverterNode()
	{
		if(currentState == NodeStates.RUNNING)
		{
			if (successState) 
			{
				currentState = NodeStates.FAILURE;
			}

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
