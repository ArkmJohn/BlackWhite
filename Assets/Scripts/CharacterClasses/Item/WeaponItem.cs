using UnityEngine;
using System.Collections;

public class WeaponItem : Item {

    public BDType basicDMGType;
    public SDType secDMGType;
    public float damageInc;
    public float atkTypeID;
    public Character owner;
    public bool isEquiped;

    
    public override void UseItem()
    {
        GameObject player = FindObjectOfType<CharacterControl>().gameObject;
        // Sets the owner of this item
        player.GetComponent<Player>().EquipWeapon(gameObject);
        owner = player.GetComponent<Character>();
        foreach (Collider a in GetComponentsInChildren<Collider>()) // Turns off all colliders for each of its children
        {
            a.enabled = false;
        }
        if(atkTypeID != 0)
            Destroy(GetComponent<Rigidbody>());

        isEquiped = true;
        FindObjectOfType<FloatUI>().UseIText("Used " + itemName); // Adds a float UI for the player
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (atkTypeID == 0 && isUsed && isEquiped)
        {
            if (col.gameObject.GetComponent<Character>())
                col.gameObject.GetComponent<Character>().GetDamaged(owner);

            Destroy(gameObject);
        }
        if (atkTypeID == 1 && isUsed && isEquiped)
        {
            if (col.gameObject.GetComponent<Character>())
                col.gameObject.GetComponent<Character>().GetDamaged(owner);
        }
    }
}
