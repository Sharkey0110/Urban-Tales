using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    //taken from input manager
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    [Header("Movement Settings")]
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float fallSpeed = 0.5f;

    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();
        if (player.IsOwner)
        {
            //send movement values to the shared network if you already have the values on your local device by being the player
            player.characterNetworkManager.networkVerticalValue.Value = verticalMovement;
            player.characterNetworkManager.networkHorizontalValue.Value = horizontalMovement;
            player.characterNetworkManager.networkMoveAmount.Value = moveAmount;
        }
        else
        {
            //take other players movement values from the network so you can see what way theyre moving
            verticalMovement = player.characterNetworkManager.networkVerticalValue.Value;
            horizontalMovement = player.characterNetworkManager.networkHorizontalValue.Value;
            moveAmount = player.characterNetworkManager.networkMoveAmount.Value;

            //then play the correct animation using said values
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
        }
    }

    //called on player manager update
    public void HandleAllMovements()
    {
        if (player.isPerformingAction || PlayerUIManager.Instance.isMenuWindowOpen)
        {
            player.characterController.Move(Vector3.zero);
            return;
        }

        HandleFreeFallMovement();
        HandleGroundedMovement();
        HandleRotation();

    }

    public void HandleGroundedMovement()
    {
        if (player.isGrounded)
        {
            //get the vertical and horizontal inputs from the character controller in player input manager
            verticalMovement = PlayerInputManager.Instance.verticalMovement;
            horizontalMovement = PlayerInputManager.Instance.horizontalMovement;
            moveAmount = PlayerInputManager.Instance.moveAmount;

            //create a move direction which causes forward to be whereever the camera is facing
            moveDirection = PlayerCamera.Instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.Instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            //since were moving a character we need a character mover

            if (moveAmount > 0.5)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (moveAmount >= 0 && moveAmount < 0.5)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }

    public void HandleRotation()
    {
        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.Instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection += PlayerCamera.Instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        //if were not moving
        if(targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = targetRotation;
    }

    public void HandleFreeFallMovement()
    {
        if(!player.isGrounded)
        {
            Vector3 freeFallDirection;
            freeFallDirection = PlayerCamera.Instance.transform.forward * PlayerInputManager.Instance.verticalMovement;
            freeFallDirection += PlayerCamera.Instance.transform.right * PlayerInputManager.Instance.horizontalMovement;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * fallSpeed * Time.deltaTime);
        }
    }
}
