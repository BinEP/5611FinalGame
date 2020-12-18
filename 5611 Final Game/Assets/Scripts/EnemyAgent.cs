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
    public bool useVecObs;
    Rigidbody2D playerRigid;
    Rigidbody2D rb;
    EnvironmentParameters m_ResetParams;
    Vector2 gravity;
    //bool[] dimensionArr;

    public override void Initialize()
    {
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
            sensor.AddObservation(gameObject.transform.position);
            sensor.AddObservation(player.transform.position - gameObject.transform.position);
            sensor.AddObservation(gravity);

            sensor.AddObservation(playerRigid.velocity);
            //Tilemap tilemap = GameObject.Find("Dim1/Walls").GetComponent<Tilemap>();
            //BoundsInt bounds = tilemap.cellBounds;
            //TileBase[] allTiles = tilemap.GetTilesBlock(bounds);




            //for (int x = 0; x < bounds.size.x; x++) {
            //    for (int y = 0; y < bounds.size.y; y++) {
            //        TileBase tile = allTiles[x + y * bounds.size.x];
            //        if (tile != null) {
            //            dimensionArr[x + y * bounds.size.x] = true;
            //        } else {
            //            dimensionArr[x + y * bounds.size.x] = false;
            //        }
            //    }
            //}
            //sensor.
            //sensor.AddObservation(dimensionArr);.
            //We don't care about dimension, screw it
        }
    }

	public override void OnActionReceived(float[] vectorAction) {

        rb.AddForce(GlobalVars.Instance.gravityScale * gravity / Time.deltaTime);

        float vecX = vectorAction[0] * GlobalVars.Instance.enemySpeed;
        float vecY = vectorAction[1] * GlobalVars.Instance.enemySpeed;
        vecX = Mathf.Clamp(vecX, -GlobalVars.Instance.enemySpeed, GlobalVars.Instance.enemySpeed);
        vecY = Mathf.Clamp(vecX, -GlobalVars.Instance.enemySpeed, GlobalVars.Instance.enemySpeed);
        rb.velocity = new Vector2(vecX, vecY);

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
        //gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        //gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        //gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        //playerRigid.velocity = new Vector3(0f, 0f, 0f);
        //player.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 4f, Random.Range(-1.5f, 1.5f))
        //    + gameObject.transform.position;
        //Reset the parameters when the Agent is reset.
        SetResetParameters();
    }

	public override void Heuristic(float[] actionsOut) {
        Vector2 move = player.transform.position - gameObject.transform.position;
        actionsOut[0] = move.x;
        actionsOut[1] = move.y;
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

        //x: -45 to 45
        //y: -15 to 30
        int xPlayer = (int)Random.Range(-45, 45);
        int yPlayer = (int)Random.Range(-15, 30);

        //x: -45 to 45
        //y: -15 to 30
        int xEnemy = (int)Random.Range(-45, 45);
        int yEnemy = (int)Random.Range(-15, 30);

        player.transform.position = new Vector2(xPlayer, yPlayer);
        gameObject.transform.position = new Vector2(xEnemy, yEnemy);
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
