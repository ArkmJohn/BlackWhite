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
    private AudioSource SoundEffect;

    void Start () {
        isDead = true;
        Health = MaxHealth;
        damage = GetComponent<Damage>();
        SoundEffect = GameObject.Find("_SCRIPTS_").GetComponent<AudioSource>();
	}


    public void GetDamaged(Character attacker)
    {
        float inc = attacker.damage.GetDamage(attacker, gameObject.GetComponent<Character>());
        Debug.Log("Inc is " + inc + " To begin");
        inc -= _def;
        Debug.Log("Inc is " + inc + " minus defenses");

        Vector3 h = attacker.gameObject.transform.position - transform.position;
        float dist = h.magnitude;
        Vector3 direction = h / dist;

        Instantiate(damageParticle, direction + transform.position, Quaternion.identity);

        Health -= inc;

        SoundEffect.Play();
        Debug.Log("Resulting health is " + Health);

        Debug.Log(" Got hit with " + inc + " by " + attacker.gameObject.name);
        if (Health <= 0)
            Death();
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);

    }
}
