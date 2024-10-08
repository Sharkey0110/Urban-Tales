using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    public Camera cameraObject;
    public PlayerManager player;
    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Settings")]
    private Vector3 cameraVelocity;
    private float cameraSmoothSpeed = 1f;

    private float maximumCameraPivot = 60f;
    private float minimumCameraPivot = -30f;

    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;

    [SerializeField] float leftAndRightRotationSpeed = 340;
    [SerializeField] float upAndDownRotationSpeed = 220;

    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask cameraCollideLayers;

    private float cameraZPosition;
    private float targetCameraZPosition;
    private Vector3 cameraObjectPosition; //For camera collisions


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObject.transform.localPosition.z;
    }

    //called on the player manager update so every second
    public void HandleAllCameraMovements()
    {
        if(player != null)
        {
            HandleFollowTarget();
            HandleRotations();
            HandleCollisions();
        }
    }

    private void HandleFollowTarget()
    {
        //calculate where the camera needs to go by moving the cameras position towards the players overtime, which keeps a constant distance
        Vector3 cameraTargetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);

        // Update the camera's position to the newly calculated target position
        transform.position = cameraTargetPosition;
    }

    private void HandleRotations()
    {
        //collect from input manager
        leftAndRightLookAngle += (PlayerInputManager.Instance.cameraHorizontalMovement * leftAndRightRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle -= (PlayerInputManager.Instance.cameraVerticalMovement * upAndDownRotationSpeed) * Time.deltaTime;

        //clamp the up and down rotation (so you cant infinitely spin
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumCameraPivot, maximumCameraPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        //rotates the x axis
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        //reset
        cameraRotation = Vector3.zero;

        //rotate the x axis
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        //pass where the camera wants to be currently
        targetCameraZPosition = cameraZPosition;

        RaycastHit hit;

        //get which direction the camera is facing
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        //create a sphere which can detect if anything is inbetween the center (camera position) and the edge of the sphere
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), cameraCollideLayers))
        {
            //if there is, change the cameras distance from the player to the distance of collision
            float distanceFromObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distanceFromObject - cameraCollisionRadius);
        }

        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }

        //smoothly alter the cameras position so that when a collision does happen the camera slides rather than snaps
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
