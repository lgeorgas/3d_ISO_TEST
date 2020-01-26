using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 10f;
    Vector3 forward, right;

	void Start () {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        //should get a vector that faces roughly -45 from world x axis....right? D:
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.anyKey)
        {
            Move();
        }

	}

    void Move()
    {
        const float turnRateAsMultipleOfSeconds = 10;

        Vector3 currentHeading = transform.forward;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 wantedHeading = Vector3.Normalize(rightMovement + upMovement);
        Vector3 heading = Vector3.Slerp(currentHeading, wantedHeading, Time.deltaTime * turnRateAsMultipleOfSeconds);
        
        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
