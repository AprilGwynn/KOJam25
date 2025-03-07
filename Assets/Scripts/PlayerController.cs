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
    private InputAction moveAction;
    
    public float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        var actionz = InputSystem.ListEnabledActions();
        Debug.Log("InputSystem.ListEnabledActions(): " + InputSystem.ListEnabledActions()[0]);
        // moveAction = InputSystem.ListEnabledActions().Find(x => x.name.ToLower() == "move");
        moveAction = InputSystem.ListEnabledActions().Find(x => true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Vector3 newMove = new Vector3(moveValue.x, moveValue.y, 0);
        playerObject.transform.position += newMove * (moveSpeed * Time.deltaTime);
    }
}
