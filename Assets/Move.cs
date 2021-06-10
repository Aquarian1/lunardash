using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public float jumpSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {

    }   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded())
        {   
            rb.AddForce(transform.up * jumpSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {   
            rb.AddForce(transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {   
            rb.AddForce(transform.right * moveSpeed);
        }
        bool isGrounded(){
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask); 
            if (hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
