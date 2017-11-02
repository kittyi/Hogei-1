using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivate : MonoBehaviour {

    [Header("Enemies")]
    [Tooltip("The enemies inside this room")]
    public GameObject[] myEnemies = new GameObject[0];

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

    }
}
