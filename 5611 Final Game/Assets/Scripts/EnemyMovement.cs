using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
   
    public float minWait;
    public float maxWait;

    GameObject player;
    bool canAttack = true;

   
    Rigidbody2D rb;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

    }

	public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.CompareTag("Player")) {
            playerInSight();
        }
    }
   
    void playerInSight()
    {
        var dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist <= GlobalVars.enemyAttackRange)
        {
            // attack
            if (canAttack)
            {
                canAttack = false;
                SoundManagerScript.playSound("attack");
                Invoke("attack", 0.5f);
               
            }   
        }

    }

    void attack() {
        GlobalVars.playerHealth -= GlobalVars.enemyAttackValue;
        Invoke("enableAttack", 1f);
    }

    void enableAttack()
    {
        canAttack = true;
    }
}
