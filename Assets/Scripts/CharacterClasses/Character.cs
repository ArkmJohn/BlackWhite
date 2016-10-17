using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public int Health;
    public float baseDmg;
    private bool isDead = false;

    public Damage damage;
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
