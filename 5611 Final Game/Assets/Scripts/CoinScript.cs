﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalVars.Instance.collectCoin();
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(GlobalVars.Instance.gravityScale * GlobalVars.Instance.gravityDir / Time.fixedDeltaTime);
        //rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
    }
}
