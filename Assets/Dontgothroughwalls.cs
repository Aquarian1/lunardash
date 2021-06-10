using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class Dontgothroughwalls : MonoBehaviour {
 
    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;  
 
    public LayerMask layerMask; //make sure we aren't in this layer
    public float skinWidth = 0.1f; //probably doesn't need to be changed
 
    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody2D myRigidbody;
    public Collider2D MyCollider;
 
 
    //initialize values
    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D> ();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(MyCollider.bounds.extents.x, MyCollider.bounds.extents.y), MyCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }
 
    void FixedUpdate()
    {
        //have we moved more than our minimum extent?
        Vector3 movementThisStep = (Vector3)myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;
     
        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;
         
            //check for obstructions we might have missed
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
             
                if (!hitInfo.collider)
                    return;
             
                if (hitInfo.collider.isTrigger)
                    hitInfo.collider.SendMessage("OnTriggerEnter", MyCollider);
             
                if (!hitInfo.collider.isTrigger)
                    myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
 
            }
        }
     
        previousPosition = myRigidbody.position;
    }
}
 