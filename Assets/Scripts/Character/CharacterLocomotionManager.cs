using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Ground Check")]
    [SerializeField] protected Vector3 yVelocity; //downwards force of gravity
    [SerializeField] protected float groundedYVelocity = -20;
    [SerializeField] protected float fallStartYVelocity = -5;
    [SerializeField] float gravityForce = 11;
    [SerializeField] LayerMask groundLayer;
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake()
    {
        //We need to check some variables on the character manager, so call it
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundCheck();
        if(character.isGrounded)
        {
            if(yVelocity.y < 0)
            {
                //if character is grounded and has a velocity below 0, which should be always because thres no jumping, then have a set -20 gravity pull
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if(!fallingVelocityHasBeenSet)
            {
                //if character has just entered the air, set velocity to -5 for a floatier fall to begin with
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            //then start adding to the falls velocity until character hits the ground and it is reset back to -20
            inAirTimer += Time.deltaTime;
            yVelocity.y -= gravityForce * Time.deltaTime;
            character.characterController.Move(yVelocity * Time.deltaTime);
        }
    }

    protected void HandleGroundCheck()
    {
        //checks a sphere around the characters feet and sees if it passes through an object on the ground layer
        character.isGrounded = Physics.CheckSphere(character.transform.position, 0.2f, groundLayer);
    }

}
