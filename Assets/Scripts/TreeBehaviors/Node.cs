using UnityEngine;
using System.Collections;

public abstract class Node
{
	// ENUM States
	public enum NodeStates
	{
        FAILURE,

        RUNNING,
		SUCCESS
		
	}
    
    public string myName;

	// Variable that stores the node states
	protected NodeStates currentState;

	public virtual void Start()
	{
		currentState = NodeStates.RUNNING;
	}

    public virtual void Add(Node node)
    { }

	public abstract void reset();

	public abstract void act(Enemy enemy);

    //private static bool isSpecial;
    public virtual bool IsSpecial { get; set; }

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
		//Debug.Log ("In Node Script: " + myName + " Has Succeeded");
		return currentState.Equals (NodeStates.SUCCESS);
	}

	public bool isFail()
	{
		//Debug.Log ("In Node Script: " + myName + " Has Failed");
		 return currentState.Equals (NodeStates.FAILURE);
	}

	public bool isRunning()
	{
		 //Debug.Log ("In Node Script: " + myName);
		 return currentState.Equals (NodeStates.RUNNING);
	}
}


