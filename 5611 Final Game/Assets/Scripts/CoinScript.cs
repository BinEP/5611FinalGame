using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool isAlive = true;
    private bool reviving = false;
    private int reviveTimer = 60;
    private int currentcount = 0;
    private Vector3 nextPos = new Vector3(0.0f, 0.0f, 0.0f);

    void Update()
    {
        if (currentcount > reviveTimer)
        {
            currentcount = 0;
            isAlive = true;
            reviving = false;
            rb.MovePosition(nextPos);
        }
        if (reviving) {
            currentcount++;
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalVars.Instance.collectCoin();
            kill();
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(GlobalVars.Instance.gravityScale * GlobalVars.Instance.gravityDir / Time.fixedDeltaTime);
        //rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
    }

    public bool IsAlive()
    {
        return (isAlive || reviving);
    }

    public void kill()
    {
        rb.MovePosition(new Vector3(-10000, -100000, 0));
        isAlive = false;
    }

    public void revive(Vector3 newPos)
    {
        nextPos = newPos;
        reviving = true;
    }
}
