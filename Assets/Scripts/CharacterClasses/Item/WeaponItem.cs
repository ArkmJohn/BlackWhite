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
        FindObjectOfType<FloatUI>().UseIText("Used " + itemName);
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (atkTypeID == 0 && isUsed && isEquiped)
        {
            Debug.Log("Arrow Collided!");
            if (col.gameObject.GetComponent<Character>())
                col.gameObject.GetComponent<Character>().GetDamaged(owner);

            Destroy(gameObject);
        }
        if (atkTypeID == 1 && isUsed && isEquiped)
        {
            Debug.Log("Melee Weapon Collided!");
            if (col.gameObject.GetComponent<Character>())
                col.gameObject.GetComponent<Character>().GetDamaged(owner);
        }
    }
}
