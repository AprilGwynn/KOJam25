using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the "movement" of the player forward,
///  but actually just controls the rotation of the map ring around the player.
/// </summary>
public class MapMovement : MonoBehaviour
{
    [SerializeField] private float DebugRotSpeed = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.right, DebugRotSpeed);
    }
}
