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
    public MapMovement mapRing;
    [FormerlySerializedAs("controlz")] public InputActionAsset controls;
    private InputAction moveeActionDebug;
    private InputAction moveLeftAction;
    private InputAction moveRightAction;
    
    public float moveSpeedX = 3f;

    public float acceleration = 10f;
    public float collisionDeceleration = 50f;
    [Tooltip("The normal top running speed that the player reaches just by avoiding obstacles")]
    public float runSpeedTop = 50f;


    public int LanePos;
    // Start is called before the first frame update
    void Start()
    {
        LanePos = mapRing.laneCount / 2;
    }

    private void OnEnable()
    {
        moveeActionDebug = controls.FindAction("Movee");
        moveLeftAction = controls.FindAction("MovLeft");
        moveRightAction = controls.FindAction("MovRight");
        moveeActionDebug.Enable();
        moveLeftAction .Enable();
        moveRightAction.Enable();
    }

    private void OnDisable()
    {
        moveeActionDebug.Disable();
        moveLeftAction.Disable();
        moveRightAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (moveLeftAction.WasPressedThisFrame())
        {
            if (LanePos >= 1) LanePos -= 1;
        }

        if (moveRightAction.WasPressedThisFrame())
        {
            if (LanePos < mapRing.laneCount-1) LanePos += 1;
        }
        
        Vector2 moveValue = moveeActionDebug.ReadValue<Vector2>();
        // Debug movement left and right smoothly
        // Vector3 newMove = new Vector3(moveValue.x, 0, 0);
        // this.transform.position += newMove * (moveSpeedX * Time.deltaTime);
        
        // Debug movement forward and back 
        if (moveValue.y > 0.2)
        {
            mapRing.StartAccelerating(acceleration);
        }

        if (moveValue.y < -0.2)
        {
            mapRing.Stop(collisionDeceleration);
        }

        DebugPlacePlayerOnLane();

    }

    private void DebugPlacePlayerOnLane()
    {
        // float posX = (((float)mapRing.sizeX / mapRing.laneCount) * LanePos) - (float)mapRing.sizeX/2;
        float posX = (((float)mapRing.sizeX / mapRing.laneCount) * LanePos) - (float) mapRing.sizeX/2 + (float)mapRing.sizeX/(mapRing.laneCount*2);
        this.transform.localPosition = new Vector3(posX, 0, 0);
    }
}
