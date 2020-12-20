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
    public GameObject player;
    [Tooltip("Whether to use vector observation. This option should be checked " +
        "in 3DBall scene, and unchecked in Visual3DBall scene. ")]
    Rigidbody2D playerRigid;
    public Rigidbody2D rb;
    EnvironmentParameters m_ResetParams;
    Vector2 gravity;
    //bool[] dimensionArr;

    public override void Initialize()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        //Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        player.GetComponent<PlayerMovement>().FakeStart();
        playerRigid = player.GetComponent<Rigidbody2D>();
        //rb = GetComponent<Rigidbody2D>();
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();
        gravity = GlobalVars.gravityDir;
        

        if (!Academy.Instance.IsCommunicatorOn) {
            this.MaxStep = 0;
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position); // size 3
        sensor.AddObservation(player.transform.position - gameObject.transform.position); // size 3
        sensor.AddObservation(gravity); // size 2

        sensor.AddObservation(playerRigid.velocity); // size 2

        //total size of observation space = 3 + 3 + 2 + 2 = 10

        //Add ray tracer collision here
        
    }

    /// <summary>
    /// Perform actions based on a vector of numbers
    /// Where [0] dictates horizontal movement with (0) being no movement, (1) being left, and (2) being right
    /// and where [1] dictates vertical movement with (0) being no movement, (1) being down, and (2) being up
    /// </summary>
	public override void OnActionReceived(float[] vectorAction) {

        if (Random.Range(0, 101) < 2) {
            RandomizeGravity();
        }

        if (Random.Range(0, 8000) < 2) {
            GameManagerScript.switchDim(Random.Range(1, GlobalVars.numDimensions + 1));
        }

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
        Vector2 speed = directionToStep * GlobalVars.enemySpeed;
        rb.velocity = speed;
        //rb.velocity = new Vector2((directionToStep * GlobalVars.enemySpeed).x, rb.velocity.y);


        String toPrint = "[ ";
        foreach (float f in vectorAction)
        {
            toPrint += f.ToString() + " ";
        }
        Debug.Log("Action Received: " + toPrint + "]");
        //Global var for now, will be local for training
        //gravity = GlobalVars.gravityDir;
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
       
        if (gameObject.transform.position.x < -50 
            || gameObject.transform.position.x > 50
            || gameObject.transform.position.y < -20
            || gameObject.transform.position.y > 35
            || player.transform.position.x < -50
            || player.transform.position.x > 50
            || player.transform.position.y < -20
            || player.transform.position.y > 35) {
            
            EndEpisode();
        }
        //

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

        if (Math.Abs(move.x) < 5)
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

        if (Math.Abs(move.y) < 5)
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
        Debug.Log("Randomizing Gravity");
        Vector2 grav = new Vector2(0, -1);
        int rand = Random.Range(0, 4);
        float angle = 0;

        if (rand < 1) {
            grav = new Vector2(-1.0f, 0.0f);
            
            //Debug.Log("Gravity right");
            angle = 270;
        } else if (rand < 2) {
            grav = new Vector2(1.0f, 0.0f);
            // Debug.Log("Gravity left");
            angle = 90;
        } else if (rand < 3) {
            grav = new Vector2(0.0f, 1.0f);
            //Debug.Log("Gravity up");
            angle = 180;
        } else if (rand < 4) {
            grav = new Vector2(0.0f, -1.0f);
            // Debug.Log("Gravity down");
            angle = 0;
        }
        player.GetComponent<PlayerMovement>().SetGravity(grav, angle);
        gravity = grav;
    }

    //Randomize player and bug starting position and gravity
    public void SetStarting()
    {
        //Set the attributes of the ball by fetching the information from the academy
        //player = GameObject.FindGameObjectWithTag("Player");
        
        //x: -45 to 45
        //y: -15 to 30
        int xPlayer = (int)Random.Range(-45, 45);
        int yPlayer = (int)Random.Range(-15, 30);

        //x: -45 to 45
        //y: -15 to 30
        int xEnemy = (int)Random.Range(-45, 45);
        int yEnemy = (int)Random.Range(-15, 30);

        player.transform.position = new Vector2(xPlayer, yPlayer);
        gameObject.transform.position = new Vector3(xEnemy, yEnemy, 3.0f);
        RandomizeGravity();
        

        //playerRigid.mass = m_ResetParams.GetWithDefault("mass", 1.0f);
        //var scale = m_ResetParams.GetWithDefault("scale", 1.0f);
        // player.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetStarting();
    }
}
