using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
    
    [SerializeField]
    float damageOut;

    Character targetChar = null;

    public BDType basicDmgType;
    public SDType secondaryDmgType;

    // Sets the type of the weapon to this class
    public void Type(WeaponItem weap)
    {
        basicDmgType = weap.basicDMGType;
        secondaryDmgType = weap.secDMGType;
    }

    public float GetDamage(Character myChar, Character targetChara)
    {
        float myDmg = 0;
        targetChar = targetChara;

        CalculateDamage(myChar);
        myDmg = damageOut;

        return myDmg;
    }

    // Calculates the damage using the weapon with damage types and the base strength || or without weapons if enemy
    protected void CalculateDamage(Character myChar)
    {
        damageOut = 0;

        float addDmg = 0;
        if (basicDmgType == BDType.NULL && secondaryDmgType == SDType.NULL)
        {
            damageOut = myChar.baseDmg;
        }
        else
        {
            if (ifWeaknessBase())
                addDmg += 2;
            if (ifWeaknessSec())
                addDmg += 3;
            if (ifResBase())
                addDmg -= 1;
            if (ifResSec())
                addDmg -= 2;

            damageOut = myChar.baseDmg + addDmg;
        }
    }

    // Looks for all the weakness and resistance using the damage types
    bool ifWeaknessBase()
    {
        if (basicDmgType == targetChar.baseWeakness)
        {
            return true;
        }

        return false;
    }

    bool ifWeaknessSec()
    {
        if (secondaryDmgType == targetChar.secWeakness)
        {
            return true;
        }

        return false;
    }

    bool ifResBase()
    {
        if (basicDmgType == targetChar.baseRes)
        {
            return true;
        }

        return false;
    }

    bool ifResSec()
    {
        if (secondaryDmgType == targetChar.secRes)
        {
            return true;
        }

        return false;
    }
}
