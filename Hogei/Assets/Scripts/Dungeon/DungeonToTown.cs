using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonToTown : MonoBehaviour {

    [Header("Scene Transition Numbers")]
    [Tooltip("Scene number for dungeon")]
    public int townSceneNum = 1;

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    //[Header("Input")]
    //[Tooltip("Key used for input")]
    //public KeyCode inputKey = KeyCode.E;

    ////control bools
    //private bool inPort = false;

    //script ref
    private WhatCanIDO canDo;

    // Use this for initialization
    void Start()
    {
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //move to dungeon
    public void MoveToTown()
    {
        //change players avalible moves
        canDo.canShoot = false;
        canDo.canAbility = false;
        //change the scene
        SceneManager.LoadScene(townSceneNum);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if other is player
    //    if (other.CompareTag(playerTag))
    //    {
    //        //set in port to true
    //        inPort = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //if other is player
    //    if (other.CompareTag(playerTag))
    //    {
    //        //set in port to false
    //        inPort = false;
    //    }
    //}
}
