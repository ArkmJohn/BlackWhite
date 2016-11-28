using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float Health, MaxHealth;
    public float baseDmg;

    // Char Hidden Stat
    [SerializeField]
    protected float _atkSpd = 0;
    //[SerializeField]
    public float _speed = 0;
    [SerializeField]
    protected float _def = 0;

    private bool isDead = false;

    public Damage damage;
    public BDType baseWeakness;
    public BDType baseRes;
    public SDType secWeakness;
    public SDType secRes;
    public GameObject damageParticle;

    void Start () {
        isDead = true;
        Health = MaxHealth;
        damage = GetComponent<Damage>();
	}


    public void GetDamaged(Character attacker)
    {
        float inc = attacker.damage.GetDamage(attacker, gameObject.GetComponent<Character>());

        inc -= _def;

        Vector3 h = attacker.gameObject.transform.position - transform.position;
        float dist = h.magnitude;
        Vector3 direction = h / dist;

        Instantiate(damageParticle, direction + transform.position, Quaternion.identity);

        Health -= inc;
        Debug.Log(" Got hit with " + inc + " by " + attacker.gameObject.name);
        if (Health <= 0)
            gameObject.SetActive(false);
    }

    public void Death()
    {
        

    }
}
