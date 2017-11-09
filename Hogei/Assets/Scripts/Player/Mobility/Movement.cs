using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Header("Remember to set the floor to the floor layor")]
    public float Speed = 0;
    Rigidbody Rigid;
    

	// Use this for initialization
	void Start () {
        Rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = Vector3.zero;
   
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            newPos.x += 1;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0f)
        {
            newPos.x -= 1;
        }
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            newPos.z += 1;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            newPos.z -= 1;
        }
        //Rigid.MovePosition(transform.position + newPos * Speed * Time.deltaTime);
        transform.position = transform.position + newPos * Speed * Time.deltaTime;
    }
}
