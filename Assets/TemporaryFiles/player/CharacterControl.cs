using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

    public float speed = 5, turnSpeed = 20;
    public Animator anim;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Punch();
        }
    }

    void FixedUpdate()
    {
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


        transform.Translate(0, 0, z);
        transform.Rotate(0, x, 0);

    }

    void Punch()
    {
        anim.SetLayerWeight(1, 1);
    }

    public void DisablePunch()
    {
        anim.SetLayerWeight(1, 0);
    }
}
