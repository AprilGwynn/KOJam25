using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float sizeX = 1;
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

    public void ReInitMapMovement()
    {
        CurrRotSpeed = 0;
        CurrAcceleration = 0;
        this.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    // Update is called once per frame
    void Update()
    {
        // accelerating
        if ((CurrAcceleration > 0 && CurrRotSpeed < player.runSpeedTop) || (CurrAcceleration < 0 && CurrRotSpeed > 0))
        {
            // accelerate || decelerate
            CurrRotSpeed += CurrAcceleration * Time.deltaTime;
        }

        // stopping if slowed to minimum speed or beyond (then starting again)
        if (CurrRotSpeed < player.stopMinimumSpeed && CurrAcceleration < 0)
        {
            Debug.Log("RotSpeed is less than stopMinimumSpeed, setting to stopMinimumSpeed and restarting accel.");
            CurrRotSpeed = player.stopMinimumSpeed;
            StartAccelerating(playerAccelCache);
        }
        
        this.gameObject.transform.Rotate(Vector3.right, CurrRotSpeed*Time.deltaTime);
    }
}
