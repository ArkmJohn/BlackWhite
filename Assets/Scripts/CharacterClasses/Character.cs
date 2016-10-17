using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public int Health;
    private bool isDead = false;

    public BDType baseWeakness;
    public BDType baseRes;
    public SDType secWeakness;
    public SDType secRes;

    void Start () {
        isDead = true;
	}


    public void GetDamage(Damage dmg)
    {

    }

    public void Death()
    {


    }
}
