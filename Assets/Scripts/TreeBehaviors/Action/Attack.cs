using UnityEngine;
using System.Collections;

public class Attack : Node 
{
	// TODO : To attack player.

	public override void reset()
	{
		Start ();
	}

	public override void act (Enemy enemy)
	{
		currentState = NodeStates.SUCCESS;
	}

}
