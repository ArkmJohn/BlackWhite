using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput; //for MOBILE SUPPORT

public class CharacterControl : MonoBehaviour
{
    public float speed = 5, turnSpeed = 35;
    public Animator anim;
    public Inventory inventory;
    public GameObject weaponHolder, hairPr;
    public Material[] hair;
    
    Rigidbody rb;

    public int attackTypeID = 1;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true, isRBC = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;

    private WalkingAudio audioRelated;

    // Use this for initialization
    void Start()
    {
		anim = GetComponent<Animator>();
        audioRelated = GameObject.FindObjectOfType<WalkingAudio>();
        weaponHolder = gameObject.GetComponentInChildren<WeaponHolder>().gameObject;
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<GameManager>().actPlayerObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Walk();
        if (Input.GetButtonDown("Jump"))
        {
            //GetComponent<Player>().Attack(GetComponent<Player>().range, GetComponent<Damage>());
            Punch();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            FindObjectOfType<PauseManager>().Paused();
        }
		
    }

	void FixedUpdate()
	{
		#if UNITY_EDITOR
			Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			WalkAnimate();
		#elif !UNITY_EDITOR
			Walk(CrossPlatformInputManager.GetAxis("Horizontal"),CrossPlatformInputManager.GetAxis("Vertical"));
			WalkAnimateM();
		#endif
	}

	void Walk (float horizontal, float vertical) 
	{
		//Debug.Log(vertical);



        if (vertical <= -0.5f)
            vertical = -0.5f;



        float x = horizontal * turnSpeed * Time.deltaTime;
        float z = vertical * speed * Time.deltaTime;
        //transform.Translate(0, 0, z);

        rb.AddForce(transform.rotation*( Vector3.forward* z*1500));
        float speedLimit = 1;
        if (rb.velocity.magnitude > speedLimit) {
            Vector3 temp = rb.velocity.normalized*speedLimit;
            temp.y = rb.velocity.y;
            rb.velocity = temp;
        }

        transform.Rotate(0, x, 0);
       if (vertical != 0)
       {
			//anim.SetFloat("Speed", vertical);    
           audioRelated.WalkingAudioScr();
           audioRelated.playsound = true;
       }
        else
        {
			
          audioRelated.WalkingAudioScrStop();
          audioRelated.playsound = false;
       }
    }

	void WalkAnimate()
	{
		float vertical = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", vertical);   

	}

	void WalkAnimateM()
	{
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");
		anim.SetFloat("Speed", vertical);   
	}

    public void SetHair(int index)
    {
        hairPr.GetComponent<Renderer>().material = hair[index];
    }

    public void Punch()
    {
        anim.SetLayerWeight(attackTypeID, 1);
        anim.SetTrigger("Attack");
    }

    public void DieAnimate()
    {
        anim.SetTrigger("IsDead");
    }
    public void DisablePunch()
    {
        if (GetComponent<Player>().equipedWeapon != null)
        {
            foreach (Collider a in GetComponent<Player>().equipedWeapon.GetComponentsInChildren<Collider>())
            {
                a.enabled = false;
            }
        }
        anim.SetLayerWeight(attackTypeID, 0);
    }

    void OnTriggerEnter(Collider obj)
    {
        if ((obj.gameObject.GetComponent<Item>() != null))
        {
            if (!obj.gameObject.GetComponent<Item>().isUsed)
            {
                //Debug.Log(obj.name + " is Collected!");

                inventory.AddItem(obj.gameObject);
                //Debug.Log("Item added in Inventory");

                //destroy item once collected
                //Destroy(obj.gameObject);
                obj.gameObject.GetComponent<Item>().isUsed = true;
                obj.gameObject.transform.SetParent(inventory.transform);
                obj.gameObject.SetActive(false);
            }

        }
        if ((obj.gameObject.GetComponent<EndGoal>() != null))
        {
            Destroy(obj.gameObject);
            FindObjectOfType<EndGameManager>().WinGame();
        }
        if (obj.gameObject.layer == 13)
        {
            GetComponent<Character>().GetDamaged(obj.gameObject.GetComponentInParent<Character>());
            obj.gameObject.SetActive(false);
        }
    }

    public void VoidHit()
    {
        Transform[] availablePositions = GameObject.Find("EmptyTileContainer").GetComponentsInChildren<Transform>();
        Vector3 randPos = availablePositions[Random.Range(0, availablePositions.Length)].transform.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(randPos.x, 5, randPos.z), 20 * Time.deltaTime);
    }


}
