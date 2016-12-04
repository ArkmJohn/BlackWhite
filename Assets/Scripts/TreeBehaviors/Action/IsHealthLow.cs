using UnityEngine;
using System.Collections;

public class IsHealthLow : Node 
{
    public IsHealthLow(string myName)
    {
        this.myName = myName;

    }
    // Resetting the node
    public override void reset()
	{
		Start();
	}

	// Checking the enemy health
	public override void act(Enemy enemy)
	{
		// If health is less then set to success state
		if(enemy.Health < (enemy.MaxHealth/2))
			SuccessState ();
		else
			// If enemy health is more then set to failure state
			FailureState();
	}

}
