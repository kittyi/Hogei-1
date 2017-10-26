using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRoomChange : MonoBehaviour {

    [Header("Door refs")]
    [Tooltip("Reference to door A")]
    public TownDoorCheck doorA;
    [Tooltip("Reference to door B")]
    public TownDoorCheck doorB;

    [Header("Tags")]
    [Tooltip("Fader tag")]
    public string fadeTag = "Fade";

    //script refs
    private FadeInFadeOut fader;

    //control vars
    private bool readyingMove = false;
    private float timeFadeStart = 0.0f;

	// Use this for initialization
	void Start () {
        fader = GameObject.FindGameObjectWithTag(fadeTag).GetComponent<FadeInFadeOut>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Move between the two rooms, based on which room detects player
    private void BeginMove()
    {
        //check two doors, if either detects player, move player to other door
        if (doorA.isPlayerPresent)
        {
            //fade in
            fader.StartFadeIn();
            //move player to other room after fade in done
            
        }
    }

    //move the player and fade out after a delay
    private IEnumerator MoveRooms(TownDoorCheck a, TownDoorCheck b)
    {
        yield return new WaitForSeconds(fader.fadeSpeed);
        
    }
}
