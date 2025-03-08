using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the "movement" of the player forward,
///  but actually just controls the rotation of the map ring around the player.
/// </summary>
public class MapMovement : MonoBehaviour
{
    // public const float DEFAULT_ACCEL = 3f;
    // public const float DEFAULT_DECEL_COLLIDE = 7f;
    
    public int laneCount = 3;
    public int sizeX = 1;
    public float CurrRotSpeed = 0;
    private float CurrAcceleration = 0f;

    private float playerAccelCache = 0f;
    private float playerDecelCache = 0f;

    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartAccelerating(float acceleration)
    {
        playerAccelCache = acceleration;
        CurrAcceleration = playerAccelCache;
    }

    public void Stop(float deceleration)
    {
        playerDecelCache = deceleration;
        CurrAcceleration = -playerDecelCache;
    }
    // public void StopHard()
    // {
    //     accelerating = false;
    //     CurrRotSpeed = 0;
    // } 
    // Update is called once per frame
    void Update()
    {
        // accelerating
        if ((CurrAcceleration > 0 && CurrRotSpeed < player.runSpeedTop) || (CurrAcceleration < 0 && CurrRotSpeed > 0))
        {
            // accelerate || decelerate
            CurrRotSpeed += CurrAcceleration * Time.deltaTime;
        }

        // stopping if slowed to 0 speed or more (then starting again)
        if (CurrRotSpeed < 0)
        {
            Debug.Log("RotSpeed is less than 0, setting to 0 and restarting accel.");
            CurrRotSpeed = 0;
            StartAccelerating(playerAccelCache);
        }
        
        this.gameObject.transform.Rotate(Vector3.right, CurrRotSpeed*Time.deltaTime);
    }
}
