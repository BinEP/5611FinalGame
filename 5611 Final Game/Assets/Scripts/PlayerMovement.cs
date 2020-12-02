using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 velocity;
    public SpriteRenderer sprite;

    void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>();
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
            GlobalVars.Instance.gravityDir = new Vector2(-1.0f, 0.0f);
            Debug.Log("Gravity right");
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            GlobalVars.Instance.gravityDir = new Vector2(1.0f, 0.0f);
            Debug.Log("Gravity left");
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            GlobalVars.Instance.gravityDir = new Vector2(0.0f, 1.0f);
            Debug.Log("Gravity up");
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            GlobalVars.Instance.gravityDir = new Vector2(0.0f, -1.0f);
            Debug.Log("Gravity down");
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(GlobalVars.Instance.gravityScale * GlobalVars.Instance.gravityDir / Time.fixedDeltaTime);
        rb.MovePosition(rb.position + velocity * GlobalVars.Instance.playerSpeed * Time.fixedDeltaTime);
    }

    public void drawCard()
    {
        print("draw card");
    }
}