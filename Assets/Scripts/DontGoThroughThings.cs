using UnityEngine;
using System.Collections;

public class DontGoThroughThings : MonoBehaviour
{
    private float minExt;
    private float partExt;
    private float sqrMinimumExtent;
    public bool sendTrigMsg = false;
    public float skinWidth = 0.3f;

    private Vector3 prevPos;
    private Rigidbody rb;
    private Collider myCol;
    public LayerMask layerMask = -1; 

    void Start()
    {
        myCol = GetComponent<Collider>();

        rb = GetComponent<Rigidbody>();
        prevPos = rb.position;

        minExt = Mathf.Min(Mathf.Min(myCol.bounds.extents.x, myCol.bounds.extents.y), myCol.bounds.extents.z);
        partExt = minExt * (2.0f - skinWidth);
        sqrMinimumExtent = minExt * minExt;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector3 movementThisStep = rb.position - prevPos;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;

            //check for obstructions we might have missed 
            if (Physics.Raycast(prevPos, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                if (!hitInfo.collider)
                    return;

                if (hitInfo.collider.isTrigger)
                    hitInfo.collider.SendMessage("OnTriggerEnter", myCol);

                if (!hitInfo.collider.isTrigger)
                    rb.position = hitInfo.point - (movementThisStep / movementMagnitude) * partExt;

            }
        }

        prevPos = rb.position;
    }
}