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
        float horizontal = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        transform.Translate(0, 0, vertical);
        transform.Rotate(0, horizontal, 0);

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
