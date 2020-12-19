using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    float nextWaypointDistance = 1.5f;
    public GameObject TargetsContainer;
    public Transform[] targets;
    public int targetIndex;
    public Transform currTarget;
    public float minWait;
    public float maxWait;

    Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = true;
    public bool selectingTarget = false;

    GameObject player;
    bool canAttack = true;

    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction;

    LayerMask visibilityMask;

    public int health;
    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setTargets();
        targetIndex = -1;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        selectingTarget = true;
        SelectNewTarget();
        InvokeRepeating("UpdatePath", 0.5f, 0.5f);
        health = GlobalVars.Instance.enemyStartingHealth;

        visibilityMask = 1 << LayerMask.NameToLayer("Obstacle");
        visibilityMask += 1; // sets bitmask for "Default"

        speed = GlobalVars.Instance.enemySpeed;
    }

    void setTargets()
    {
        Transform[] transforms = TargetsContainer.GetComponentsInChildren<Transform>();
        targets = new Transform[transforms.Length - 1];
        int i = 0;
        foreach (Transform t in transforms)
        {
            if (t.position != TargetsContainer.transform.position)
            {
                targets[i++] = t;
            }
        }
    }

    void UpdatePath()
    {
        if (reachedEndOfPath && !selectingTarget)
        {
            selectingTarget = true;
            path = null;
            Invoke("SelectNewTarget", Random.Range(minWait, maxWait));
        }
        if (!reachedEndOfPath && seeker.IsDone())
        {
            seeker.StartPath(rb.position, currTarget.position, OnPathComplete);
        }
    }
    
    void FixedUpdate()
    {
        if (direction != null)
        {
            checkForPlayer();
        }

        // skip moving if no path available
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            rb.velocity = new Vector2(0, 0);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log("PLEASE DONE RUN");
        rb.AddForce(GlobalVars.Instance.gravityScale * GlobalVars.Instance.gravityDir / 2);
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void checkForPlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, visibilityMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                playerInSight();
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void SelectNewTarget()
    {
        if (path != null) return;

        targetIndex = targetIndex >= (targets.Length - 1) ? 0 : targetIndex + 1;
        currTarget = targets[targetIndex];
        reachedEndOfPath = false;
        selectingTarget = false;
        speed = GlobalVars.Instance.enemySpeed;

        seeker.StartPath(rb.position, currTarget.position, OnPathComplete);
    }

    void playerInSight()
    {
        var dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist <= GlobalVars.Instance.enemyAttackRange)
        {
            // attack
            if (canAttack)
            {
                canAttack = false;
                SoundManagerScript.playSound("attack");
                Invoke("attack", 0.5f);
               
            }   
        }

        var angle = Vector2.Angle(direction, player.transform.position - transform.position);

        if (angle < GlobalVars.Instance.enemyVisiblityAngle)
        {
            if (dist <= GlobalVars.Instance.getEnemyVisibility())
            {
                //print(dist);
                // change target
                speed = GlobalVars.Instance.enemyAggroSpeed;
                currTarget = player.transform;
                reachedEndOfPath = false;
                selectingTarget = false;
            }
        } else
            if (dist <= GlobalVars.Instance.getEnemyRearVisibility())
            {
                //print(dist);
                // change target
                speed = GlobalVars.Instance.enemyAggroSpeed;
                currTarget = player.transform;
                reachedEndOfPath = false;
                selectingTarget = false;
            }

        Debug.DrawLine(transform.position, (transform.position + Vector3.right * GlobalVars.Instance.getEnemyRearVisibility()), Color.white);
        Debug.DrawLine(transform.position, (transform.position + Vector3.left * GlobalVars.Instance.getEnemyVisibility()), Color.green);
    }

    void attack() {
        GlobalVars.Instance.playerHealth -= GlobalVars.Instance.enemyAttackValue;
        Invoke("enableAttack", 1f);
    }

    void enableAttack()
    {
        canAttack = true;
    }
}
