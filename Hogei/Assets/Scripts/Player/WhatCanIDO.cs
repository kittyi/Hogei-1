using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatCanIDO : MonoBehaviour {

    [Header("Control bools")]
    public bool canMove = false;
    public bool canShoot = false;
    public bool canAbility = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
