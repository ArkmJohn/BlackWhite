using UnityEngine;
using System.Collections;

public class tempPlayer : MonoBehaviour
{
    public float speed;

    public Inventory inventory;

    // Use this for initialization
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        //calculate movement
        float movement = speed * Time.deltaTime;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * movement, 0, Input.GetAxis("Vertical") * movement));
    }

    
    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.GetComponent<Item>() != null)
        {
            inventory.AddItem(obj.gameObject);
            //Debug.Log("Item added in Inventory");

            //destroy item once collected
            //Destroy(obj.gameObject);
            obj.gameObject.SetActive(false);

        }
    }
}
