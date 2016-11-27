using UnityEngine;
using System.Collections;

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
        audioRelated = GameObject.FindObjectOfType<WalkingAudio>();
        weaponHolder = gameObject.GetComponentInChildren<WeaponHolder>().gameObject;
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<GameManager>().actPlayerObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetComponent<Player>().Attack(GetComponent<Player>().range, GetComponent<Damage>());
            Punch();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<PauseManager>().Paused();
        }

    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical <= -0.5f)
            vertical = -0.5f;

        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
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
            
           audioRelated.WalkingAudioScr();
           audioRelated.playsound = true;
       }
        else
        {
            
          audioRelated.WalkingAudioScrStop();
          audioRelated.playsound = false;
       }
    }


    public void SetHair(int index)
    {
        hairPr.GetComponent<Renderer>().material = hair[index];
    }

    void Punch()
    {
        anim.SetLayerWeight(attackTypeID, 1);
        anim.SetTrigger("Attack");
    }

    public void DisablePunch()
    {
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
                obj.gameObject.transform.SetParent(gameObject.transform);
                obj.gameObject.SetActive(false);
            }

        }
        if ((obj.gameObject.GetComponent<EndGoal>() != null))
        {
            Destroy(obj.gameObject);
            FindObjectOfType<EndGameManager>().WinGame();
        }
    }

    public void VoidHit()
    {
        Transform[] availablePositions = GameObject.Find("EmptyTileContainer").GetComponentsInChildren<Transform>();
        Vector3 randPos = availablePositions[Random.Range(0, availablePositions.Length)].transform.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(randPos.x, 5, randPos.z), 20 * Time.deltaTime);
    }
}
