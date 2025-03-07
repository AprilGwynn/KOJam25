using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// takes in the player's inputs and sends commands to the map ring to "move"
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject playerObject;
    public InputActionAsset controlz;
    private InputAction moveAction;
    
    public float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        moveAction = controlz.FindAction("Movee");
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Vector3 newMove = new Vector3(moveValue.x, 0, 0);
        playerObject.transform.position += newMove * (moveSpeed * Time.deltaTime);
        // Debug.Log("newMove: "+newMove);
    }
}
