using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 velocity;
    public SpriteRenderer sprite;
    public Vector2 gravity = new Vector2(0.0f, 1.0f);

    void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        gravity = GlobalVars.Instance.gravityDir;
    }
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(horizontal, Input.GetAxisRaw("Vertical")).normalized;

        if (horizontal > 0) {
            sprite.flipX = true;
        } else if (horizontal < 0) {
            sprite.flipX = false;
        }
        

        if (Input.GetKey(KeyCode.Escape)) {
            Debug.Log("Quittng");
            Application.Quit();
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            gravity = new Vector2(-1.0f, 0.0f);
            //transform.rotation = Quaternion.Euler(0, 0, 90);
            RotateStuff(270);
            //Debug.Log("Gravity right");
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            gravity = new Vector2(1.0f, 0.0f);
            RotateStuff(90);
            // Debug.Log("Gravity left");
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            gravity = new Vector2(0.0f, 1.0f);
            //Debug.Log("Gravity up");
            RotateStuff(180);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            gravity = new Vector2(0.0f, -1.0f);
            // Debug.Log("Gravity down");
            RotateStuff(0);
        } else if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(-1 * GlobalVars.Instance.playerJump * GlobalVars.Instance.gravityScale * gravity);
        }
        //Test Change
        GlobalVars.Instance.gravityDir = gravity;

        rb.AddForce(GlobalVars.Instance.gravityScale * gravity);
        Vector2 movementDir = Vector2.Perpendicular(horizontal * gravity * GlobalVars.Instance.playerSpeed);

        float speedMax = GlobalVars.Instance.playerMaxFallSpeed;
        if(gravity.x == 0.0f) {
            rb.velocity = new Vector2(movementDir.x, Mathf.Clamp(rb.velocity.y, -speedMax, speedMax));
        } else {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedMax, speedMax), movementDir.y);
        }

        
    }

    private void RotateStuff(float rotation) {
        sprite.transform.eulerAngles = new Vector3(
            sprite.transform.eulerAngles.x,
            sprite.transform.eulerAngles.y,
            rotation
        );

       
    }

    public void drawCard()
    {
        print("draw card");
    }
}