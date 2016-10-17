using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
    
    public int damageType;
    private float damage;
    public float weaponDamage;
    private enum BDType
    {
        NULL,
        SLICE,
        IMPACT,
        PIERCE

    }
    private enum SDType
    {
        NULL,
        ELECTRIC,
        ICE,
        FIRE
    }




    public void CalculateDamage()
    {
       
    }

    public void GetDamage(int damaged)
    {

    }

    public void SetDamage()
    {


    }
}
