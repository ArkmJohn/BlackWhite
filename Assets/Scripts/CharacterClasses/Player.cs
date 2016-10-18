using UnityEngine;
using System.Collections.Generic;

public class Player : Character {
 
    public GameObject inventoryObj;
    public GameObject equipedWeapon;
    public List<float> statistics = new List<float>();

    public void InStats(float Str,float Vit, float Dex, float End, float Res, float Intel )
    {
        Vit = statistics[0];
        End = statistics[1];
        Str = statistics[2];
        Dex = statistics[3];
        Res = statistics[4];
        Intel = statistics[5];
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
