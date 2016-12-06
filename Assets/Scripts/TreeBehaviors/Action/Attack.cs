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
        enemy.gameObject.transform.LookAt(new Vector3(enemy.target.gameObject.transform.position.x, enemy.transform.position.y, enemy.target.gameObject.transform.position.z)); 
        enemy.AttackInFront();
            SuccessState();
	}

}
