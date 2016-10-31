using UnityEngine;
using System.Collections;

public class WeaponItem : Item {

    public BDType basicDMGType;
    public SDType secDMGType;
    public float damageInc;
    public float atkTypeID;
    public Character owner;
    public bool isEquiped;

    // Swap Item from equip
    public override void UseItem()
    {
        GameObject player = FindObjectOfType<CharacterControl>().gameObject;

        player.GetComponent<Player>().EquipWeapon(gameObject);
        owner = player.GetComponent<Character>();
        foreach (Collider a in GetComponentsInChildren<Collider>())
        {
            a.enabled = false;
        }
        if(atkTypeID != 0)
            Destroy(GetComponent<Rigidbody>());

        isEquiped = true;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided!");
        if (atkTypeID == 0 && isUsed && isEquiped)
        {
            if (col.gameObject.GetComponent<Character>())
                col.gameObject.GetComponent<Character>().GetDamaged(owner);

            Destroy(gameObject);
        }
    }
}
