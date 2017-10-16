using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 Direction = MouseTarget.GetWorldMousePos() - transform.position;
        //Player Rotations
        if (Vector3.Dot(transform.right, Direction) < 0.0f)
        {
            transform.Rotate(0.0f, -Vector3.Angle(transform.forward, Direction), 0.0f);
        }
        if (Vector3.Dot(transform.right, Direction) > 0.0f)
        {
            transform.Rotate(0.0f, Vector3.Angle(transform.forward, Direction), 0.0f);
        }
        print(MouseTarget.GetWorldMousePos());
    }
}
