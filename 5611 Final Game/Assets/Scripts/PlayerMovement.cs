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
            //Debug.Log("Gravity right");
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            gravity = new Vector2(1.0f, 0.0f);
           // Debug.Log("Gravity left");
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            gravity = new Vector2(0.0f, 1.0f);
            //Debug.Log("Gravity up");
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            gravity = new Vector2(0.0f, -1.0f);
           // Debug.Log("Gravity down");
        } else if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-10 * GlobalVars.Instance.gravityScale * gravity);
        }
    }

    private void FixedUpdate()
    {
        gravity = GlobalVars.Instance.gravityDir;
        rb.AddForce(GlobalVars.Instance.gravityScale * gravity);
        rb.velocity = velocity * GlobalVars.Instance.playerSpeed;
    }

    public void drawCard()
    {
        print("draw card");
    }
}