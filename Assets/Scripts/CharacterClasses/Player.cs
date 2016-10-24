using UnityEngine;
using System.Collections.Generic;

public class Player : Character {
 
    public GameObject inventoryObj;
    public GameObject equipedWeapon;
    public List<float> statistics = new List<float>();

    public void InStats(float Str,float Vit, float Dex, float End, float Res, float Intel )
    {
        statistics[0] = Vit;
        statistics[1] = End;
        statistics[2] = Str;
        statistics[3] = Dex;
        statistics[4] = Res;
        statistics[5] = Intel;
        setBaseDmg();
    }

    void changeStat(int statID, float statVal)
    {
        statistics[statID] += statVal;
        setBaseDmg();
    }

    void setBaseDmg()
    {
        baseDmg = statistics[2];
    }

    void FixedUpdate () {
	
	}
}
