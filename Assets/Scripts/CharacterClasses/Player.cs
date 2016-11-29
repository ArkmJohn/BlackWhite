using UnityEngine;
using System.Collections.Generic;

public class Player : Character {

    public enum AttackMode
    {
        RANGE,
        MELEE
    }

    AttackMode myAtkMode = AttackMode.MELEE;

    public GameObject equipedWeapon, bowWeapon, spawnPoint;
    public List<float> statistics = new List<float>();

    //Player Hidden Stats
    public float _stam = 0;
    float _weightLimit = 0;
    float _wearInt = 0;
    public float range = 2;
    GameObject weaponHolder;

    public void InStats(float Vit, float End, float Str, float Dex, float Res, float Intel )
    {
        statistics[0] = Vit;
        statistics[1] = End;
        statistics[2] = Str;
        statistics[3] = Dex;
        statistics[4] = Res;
        statistics[5] = Intel;
        Health = statistics[0] + 10;
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
        //Health = MaxHealth;

        _stam = statistics[1] + 5;

        _weightLimit = statistics[1];

        baseDmg = statistics[2];

        _atkSpd = 0.2f + (statistics[3] / 2);

        _speed = (statistics[3] / 2) + 10;
        if(GetComponent<CharacterControl>() != null)
            GetComponent<CharacterControl>().speed = _speed;

        _def = statistics[4] / 2;

        _wearInt = statistics[5];

    }

    public void EquipWeapon(GameObject weaponObj)
    {
        WeaponItem weapon = weaponObj.GetComponent<WeaponItem>();

        // Ads the equiped weapon on the inventory then makes the variable null
        if (equipedWeapon != null)
        {
            GetComponent<CharacterControl>().inventory.AddItem(equipedWeapon);
            equipedWeapon.GetComponent<WeaponItem>().owner = null;
            equipedWeapon.GetComponent<WeaponItem>().isEquiped = false;
            equipedWeapon.SetActive(false);
            equipedWeapon.transform.SetParent(null);
            equipedWeapon = null;
        }


        if (weaponHolder == null)
            weaponHolder = GetComponent<CharacterControl>().weaponHolder;

        // Sets the damage type for animating and using attack function
        if (weapon.atkTypeID == 0)
        {
            myAtkMode = AttackMode.RANGE;
            bowWeapon.SetActive(true);
            //bowWeapon.transform.localPosition = new Vector3(0, 0, 0);
            equipedWeapon = weaponObj;
            GetComponent<CharacterControl>().attackTypeID = 2;
        }
        else
        {
            myAtkMode = AttackMode.MELEE;
            bowWeapon.SetActive(false);

            // Sets the weapon object as a child
            weaponObj.SetActive(true);
            weaponObj.transform.SetParent(weaponHolder.transform);
            weaponObj.transform.localPosition = new Vector3(0, 0, 0);
            weaponObj.transform.localEulerAngles = new Vector3(0, 45, 0);
            weaponObj.GetComponent<Collider>().enabled = false;
            equipedWeapon = weaponObj;
            GetComponent<CharacterControl>().attackTypeID = 1;

        }

        // then calculates and changes the damage
        damage.Type(weapon);
    }

    public void Attack(float range, Damage dmg)
    {
        if (myAtkMode == AttackMode.MELEE)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, forward, out hit, range))
            {
                GameObject targetHit = hit.collider.gameObject;
                if (targetHit.GetComponent<Character>() != null)
                    targetHit.GetComponent<Character>().GetDamaged(gameObject.GetComponent<Character>());
            }


        }
        else if (myAtkMode == AttackMode.RANGE)
        {
            GameObject arrow = Instantiate(equipedWeapon, spawnPoint.transform.position, transform.rotation) as GameObject;
            Physics.IgnoreCollision(arrow.GetComponent<Collider>(), GetComponent<Collider>());
            arrow.SetActive(true);
            arrow.GetComponent<Collider>().enabled = true;
            arrow.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.position + spawnPoint.transform.forward * 2000);

        }
        else
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, forward, out hit, range))
            {
                GameObject targetHit = hit.collider.gameObject;
                if (targetHit.GetComponent<Character>() != null)
                    targetHit.GetComponent<Character>().GetDamaged(gameObject.GetComponent<Character>());
            }
        }
    }

    public void FireArrow()
    {
        if (myAtkMode == AttackMode.RANGE)
        {
            Debug.Log("Arrow Fired");
            GameObject arrow = Instantiate(equipedWeapon, spawnPoint.transform.position, transform.rotation) as GameObject;
            Physics.IgnoreCollision(arrow.GetComponent<Collider>(), GetComponent<Collider>());
            arrow.SetActive(true);
            arrow.GetComponent<Collider>().enabled = true;
            arrow.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.position + spawnPoint.transform.forward * 2000);

        }
    }

    public void ActivateMeleeWeapon()
    {
        if (myAtkMode == AttackMode.MELEE)
        {
            equipedWeapon.GetComponent<Collider>().enabled = true;
        }

    }

    void FixedUpdate () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 smokes = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Gizmos.DrawLine(smokes, smokes + (transform.forward * range));
    }
}
