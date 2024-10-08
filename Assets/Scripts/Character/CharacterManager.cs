using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterManager : NetworkBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [HideInInspector] public CharacterNetworkManager characterNetworkManager;

    [Header("Flags")]
    public bool isPerformingAction;
    public bool isGrounded;

    protected virtual void Awake()
    {
        //since the player is a clone of a prefab, and not a prefab, you use the this keyword, rather than gameObject
        DontDestroyOnLoad(this);

        //we can automatically get the other components as they are all components on the same object
        characterController = GetComponent<CharacterController>(); 
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        //if we are controlling the character, assign its network position to our local position(so people can see where we are)
        if (IsOwner)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        //if were not controlling the character take its network position (passed by their device) and give it to the clone of that player on our device
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position,characterNetworkManager.networkPosition.Value, ref characterNetworkManager.networkPositionVelocity, characterNetworkManager.networkPositionSmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, characterNetworkManager.networkRotation.Value, characterNetworkManager.networkRotationSmoothTime);
        }
    }

    protected virtual void LateUpdate()
    {

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

}
