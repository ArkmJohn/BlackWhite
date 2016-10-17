using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
    
    public int damageType;
    Character targetChar;
    BDType basicDmgType;
    SDType secondaryDmgType;

    public void Type() { }

    protected void CalculateDamage()
    {
        
    }

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
