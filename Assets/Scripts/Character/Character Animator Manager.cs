using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }


    //models need to access horizontal and vertical so that they can communicate with the blend tree
    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
        character.animator.SetFloat("Horizontal", horizontalMovement, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", verticalMovement, 0.1f, Time.deltaTime);
    }

    //we will need a perform action animation and use the isperformingaction to stop players from moving or doing anything else if they are emoting or picking up item most likely
}
