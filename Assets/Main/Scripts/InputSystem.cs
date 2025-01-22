using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private PlayerInputAction playerInputActions;
    

    private void Awake() {
        playerInputActions = new PlayerInputAction();
    }

    private void OnEnable() {
        playerInputActions.Player.Enable();
    }
    private void OnDisable() {
        playerInputActions.Player.Disable();
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
