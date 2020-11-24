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
		}
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * GlobalVars.Instance.playerSpeed * Time.fixedDeltaTime);
    }

    public void drawCard()
    {
        print("draw card");
    }
}