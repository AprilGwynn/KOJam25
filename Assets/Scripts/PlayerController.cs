using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/// <summary>
/// takes in the player's inputs and sends commands to the map ring to "move"
/// </summary>
public class PlayerController : MonoBehaviour
{
    // public GameObject playerObject;
    public PlayerValues playerValues;
    public Animator playerAnimator;
    public MapMovement mapRing;
    public MapRingAssembler mapRingAssembler;
    public InputActionAsset controls;
    private InputAction moveAction;
    
    // public float moveSpeedX = 3f;

    public float acceleration = 10f;
    public float collisionDeceleration = 50f;
    [Tooltip("The normal top running speed that the player reaches just by avoiding obstacles")]
    public float runSpeedTop = 50f;
    public float stopMinimumSpeed = 10f; 

    public int currLane; // the index of which "lane" the player is in
    private int prevLane = -1; // lane the player is currently leaving (set to -1 if not currently lerping between lanes) 
    private float timeStartedLaneMove; // the time when the player started to move out of a lane
    public float LANE_MOVE_LERP_TIME = 0.1f; // how long it takes to move between lanes (move visually, logic is instant)
                // NOTE: maybe this is halved when moving twice?
                // (to provide further clarity that the move is accepted) idk it's pretty fast already..
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitLanePos();
        mapRing.StartAccelerating(acceleration);
    }

    public void InitLanePos()
    {
        currLane = mapRing.laneCount / 2;
    }
    private void OnEnable()
    {
        moveAction = controls.FindAction("Move");
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit somethin");
        Interactable theOther = other.gameObject.GetComponent<Interactable>();
        if (theOther && theOther.itemType == Interactable.InteractableType.ring
            && !(playerValues.isInvul && theOther.isNewItem))
        {
            CollectRing();
            theOther.CollideHappens();
        }

        if (theOther && theOther.itemType == Interactable.InteractableType.wall
            && !playerValues.isInvul)
        {
            mapRing.Stop(collisionDeceleration);
            playerValues.HitObstacle(theOther);
            theOther.CollideHappens();
        }
    }

    private void CollectRing()
    {
        playerValues.AddRings(1);
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector2 moveValue = moveAction.ReadValue<Vector2>();


        if (moveAction.WasPerformedThisFrame())
        {
            prevLane = currLane;
            timeStartedLaneMove = Time.time;
            if (moveValue.x < -.1)
            {
                if (currLane >= 1) currLane -= 1;
            }

            if (moveValue.x > .1)
            {
                if (currLane < mapRing.laneCount - 1) currLane += 1;
            }
        }

        DebugPlacePlayerOnLane();

    }

    // takes a lane index and returns its local X position 
    private float LaneIndexToLanePos(int laneIndex)
    {
        float lanePosX = ((mapRing.sizeX / mapRing.laneCount) * laneIndex) -  mapRing.sizeX/2 + mapRing.sizeX/(mapRing.laneCount*2);
        return lanePosX;
    }

    
    private void DebugPlacePlayerOnLane()
    {
        
        float startLanePosX = LaneIndexToLanePos(prevLane == -1 ? currLane : prevLane);
        float targetLanePosX = LaneIndexToLanePos(currLane);


        float laneCrossingProgress = (Time.time - timeStartedLaneMove) / LANE_MOVE_LERP_TIME;
        float newX = Mathf.Lerp(startLanePosX, targetLanePosX,  laneCrossingProgress);
        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        
        if (laneCrossingProgress >= 1)
        {// player has reached lane location
            prevLane = -1;
        }

    }
}
