using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Transform particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return;
            print("shooting");
            // rotate
            //print(Input.mousePosition);
            //print(particleSystem.rotation);
            //print((Input.mousePosition - particleSystem.position).rotation);
            //print(Vector2.Angle(particleSystem.position, Input.mousePosition - particleSystem.position));


            var mousePos = Input.mousePosition;
            mousePos.z = 15.0f; //The distance from the camera to the player object
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            print(Quaternion.AngleAxis(angle, Vector3.forward));
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            gameObject.GetComponentInChildren<ParticleSystem>().Play();

            print(Vector2.up * 90);
        }
    }
}
