using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashTime;
    public float dashSpeed;
    public float dashCooldown;
    private float dashTimeRemaining;
    private bool canDash;
    public Rigidbody2D rb;
    private Vector2 direction;
    private float dashCooldownInitial;
    // Start is called before the first frame update
    void Start()
    {
        dashCooldownInitial = dashCooldown;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Dash Orb")){
            dashCooldown = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashCooldown > 0){
            dashCooldown -= Time.deltaTime;
            canDash = false;
        }
        else
        {
            canDash = true;
        }
        if (dashTimeRemaining > 0.001){
            rb.velocity = direction * dashSpeed;
            dashTimeRemaining -= Time.deltaTime;
        }
        if (dashTimeRemaining > -0.01 && dashTimeRemaining < 0.0011){
            rb.velocity = direction * 0;
            dashTimeRemaining -= Time.deltaTime;
        }
        else
        {
            // rb.velocity = previousVelocity;
            if (Input.GetKeyDown("space"))
            {   
                if (canDash == true){
                    dashTimeRemaining = dashTime;
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    direction = new Vector2(
                    mousePosition.x - transform.position.x,
                    mousePosition.y - transform.position.y
                    );
                    direction.Normalize();
                    dashCooldown = dashCooldownInitial;
                }
            }
        }
    }
}
