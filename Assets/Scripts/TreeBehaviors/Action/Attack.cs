using UnityEngine;
using System.Collections;

public class Attack : Node 
{
    public Attack(string myName)
    {
        this.myName = myName;

    }

    public override void reset()
	{
		Start ();
	}

	public override void act (Enemy enemy)
	{
        enemy.gameObject.transform.LookAt(enemy.target.gameObject.transform); 
        enemy.AttackInFront();

        if(!enemy.isAttacking)
            SuccessState();
	}

}
