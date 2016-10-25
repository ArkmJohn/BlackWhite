using UnityEngine;
using System.Collections.Generic;

public class Player : Character {

    public GameObject equipedWeapon;
    public List<float> statistics = new List<float>();

    //Player Hidden Stats
    public float _stam = 0;
    float _weightLimit = 0;
    float _wearInt = 0;

    public void InStats(float Str,float Vit, float Dex, float End, float Res, float Intel )
    {
        statistics[0] = Vit;
        statistics[1] = End;
        statistics[2] = Str;
        statistics[3] = Dex;
        statistics[4] = Res;
        statistics[5] = Intel;
        setBaseStat();
    }

    public void changeStat(int statID, float statVal)
    {
        statistics[statID] += statVal;
        setBaseStat();
    }

    void setBaseStat()
    {
        MaxHealth = statistics[0] + 10;
        Health = MaxHealth;

        _stam = statistics[1] + 5;

        _weightLimit = statistics[1];

        baseDmg = statistics[2];

        _atkSpd = 0.2f + (statistics[3] / 2);

        _speed = (statistics[3] / 2) + 1;
        if(GetComponent<CharacterControl>() != null)
            GetComponent<CharacterControl>().speed = _speed;

        _def = statistics[4] / 2;

        _wearInt = statistics[5];

    }

    public void EquipWeapon(WeaponItem weapon)
    {
        // Do some stuff with the prefab

        // then calculates and changes the damage
        damage.Type(weapon);
    }

    void FixedUpdate () {
	
	}
}
