using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public int Health;
    private bool isDead = false;
	void Start () {
        isDead = true;
	}


    public void GetDamage(DamageScript dmg)
    {

    }

    public void Death()
    {


    }
}
