using System;
using UnityEngine;
using Unity.MLAgents;
//using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class EnemyAgent : Agent
{
    [Header("Enemy Agents")]
    GameObject player;
    [Tooltip("Whether to use vector observation. This option should be checked " +
        "in 3DBall scene, and unchecked in Visual3DBall scene. ")]
    public bool useVecObs;
    Rigidbody2D playerRigid;
    Rigidbody2D rb;
    EnvironmentParameters m_ResetParams;
    Vector2 gravity;
    //bool[] dimensionArr;

    public override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();

        if (!Academy.Instance.IsCommunicatorOn) {
            this.MaxStep = 0;
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (useVecObs)
        {
            sensor.AddObservation(gameObject.transform.position); // size 3
            sensor.AddObservation(player.transform.position - gameObject.transform.position); // size 3
            sensor.AddObservation(gravity); // size 2

            sensor.AddObservation(playerRigid.velocity); // size 2

            //total size of observation space = 3 + 3 + 2 + 2 = 10

           //Add ray tracer collision here
        }
    }

    /// <summary>
    /// Perform actions based on a vector of numbers
    /// Where [0] dictates horizontal movement with (0) being no movement, (1) being left, and (2) being right
    /// and where [1] dictates vertical movement with (0) being no movement, (1) being down, and (2) being up
    /// </summary>
	public override void OnActionReceived(float[] vectorAction) {

        float actionHoriz = vectorAction[0];
        float actionVert = vectorAction[1];

		Vector2 directionToStep = new Vector2(0f, 0f);
        //step left
        if (actionHoriz == 2f)
        {
            directionToStep += new Vector2(1f, 0f);
        }
        //step right
        else if (actionHoriz == 1f)
        {
            directionToStep += new Vector2(-1f, 0f);
        }            //step left
        if (actionVert == 2f)
        {
            directionToStep += new Vector2(0f, 1f);
        }
        //step right
        else if (actionVert == 1f)
        {
            directionToStep += new Vector2(0f, -1f);
        }

        directionToStep.Normalize();
        rb.velocity = directionToStep * GlobalVars.enemySpeed;
        //rb.velocity = new Vector2((directionToStep * GlobalVars.enemySpeed).x, rb.velocity.y);


        String toPrint = "[ ";
        foreach (float f in vectorAction)
        {
            toPrint += f.ToString() + " ";
        }
        Debug.Log("Action Received: " + toPrint + "]");
        //Global var for now, will be local for training
        gravity = GlobalVars.gravityDir;
        rb.AddForce(GlobalVars.gravityScale * gravity);

        //Vector2 doThing = new Vector2(vectorAction[0], vectorAction[1]);
        //doThing.Normalize();
        //doThing *= GlobalVars.enemySpeed;
        //gameObject.transform.position += doThing * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + doThing * Time.fixedDeltaTime);

        float distanceToPlayer = (player.transform.position - gameObject.transform.position).magnitude;
        if (distanceToPlayer < 10) {
            SetReward(1f);
        } else {
            SetReward(-1 * distanceToPlayer / 10);
        }
       
        //EndEpisode();

    }

    public override void OnEpisodeBegin()
    {
        SetResetParameters();
    }

    /// <summary>
    /// actionsOut has the structure of vectorAction in OnActionReceived
    /// Where [0] dictates horizontal movement with (0) being no movement, (1) being left, and (2) being right
    /// and where [1] dictates vertical movement with (0) being no movement, (1) being down, and (2) being up
    /// </summary>
	public override void Heuristic(float[] actionsOut) {

        //String toPrint = "[ ";
        //foreach (float f in actionsOut)
        //{
        //    toPrint += f.ToString() + " ";
        //}
        //Debug.Log(toPrint + "]");
        Vector2 move = player.transform.position - gameObject.transform.position;

        if (Math.Abs(move.x) < 1)
        {
            actionsOut[0] = 0f;
        }
        else
        {
            if (move.x > 0)
            {
                actionsOut[0] = 2f;
            }
            else
            {
                actionsOut[0] = 1f;
            }
        }

        if (Math.Abs(move.y) < 1)
        {
            actionsOut[1] = 0f;
        }
        else
        {
            if (move.y > 0)
            {
                actionsOut[1] = 2f;
            }
            else
            {
                actionsOut[1] = 1f;
            }
        }

        

    }

    public void RandomizeGravity() {

        Vector2 grav = new Vector2(0, -1);
        int rand = Random.Range(0, 4);

        if (rand < 1) {
            grav = new Vector2(-1.0f, 0.0f);
            //Debug.Log("Gravity right");
        } else if (rand < 2) {
            grav = new Vector2(1.0f, 0.0f);
            // Debug.Log("Gravity left");
        } else if (rand < 3) {
            grav = new Vector2(0.0f, 1.0f);
            //Debug.Log("Gravity up");
        } else if (rand < 4) {
            grav = new Vector2(0.0f, -1.0f);
            // Debug.Log("Gravity down");
        }

        player.GetComponent<PlayerMovement>().gravity = grav;
        gravity = grav;
    }

    //Randomize player and bug starting position and gravity
    public void SetStarting()
    {
        //Set the attributes of the ball by fetching the information from the academy
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        //x: -45 to 45
        //y: -15 to 30
        int xPlayer = (int)Random.Range(-45, 45);
        int yPlayer = (int)Random.Range(-15, 30);

        //x: -45 to 45
        //y: -15 to 30
        int xEnemy = (int)Random.Range(-45, 45);
        int yEnemy = (int)Random.Range(-15, 30);

        //player.transform.position = new Vector2(xPlayer, yPlayer);
        gameObject.transform.position = new Vector3(xEnemy, yEnemy, 3.0f);
        //RandomizeGravity();
        

        //playerRigid.mass = m_ResetParams.GetWithDefault("mass", 1.0f);
        //var scale = m_ResetParams.GetWithDefault("scale", 1.0f);
        // player.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetStarting();
    }
}
