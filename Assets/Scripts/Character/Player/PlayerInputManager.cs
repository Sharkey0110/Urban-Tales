using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;
    public PlayerManager player;

    //thing that stores all the keybinds
    PlayerControls playerControls;

    [Header("Movement")]
    [SerializeField] Vector2 movementInput;
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    [Header("Camera")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalMovement;
    public float cameraHorizontalMovement;

    [Header("Menu")]
    public bool openCloseMenuInput = false;
    public bool isMenuOpen = false;


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

        //this is ran on every scene change cause the add sign or something
        SceneManager.activeSceneChanged += OnSceneChange;

        //we want the player controls to start disabled so that players cant walk around while in menus/creation
        Instance.enabled = false;
        if(playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        //if the current build index is not 0 (the main menu) enable controls
        if(newScene.buildIndex != 0)
        {
            Instance.enabled = true;

            //if we have player controls, enable them too
            if(playerControls != null)
            {
                playerControls.Enable();
            }
        }
        else
        {
            Instance.enabled = false;
        }
    }

    //will trigger when the manager gets enabled
    private void OnEnable()
    {
        if(playerControls == null)
        {
            //make a new player controls for some reason
            playerControls = new PlayerControls();

            //MOVEMENT
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            //CAMERA
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            //ACTIONS
            playerControls.PlayerActions.OpenMenu.performed += i => openCloseMenuInput = true;


        }
        playerControls.Enable();
    }

    private void OnDestroy()
    {
        //for data security or something
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        //you cant read inputs if game is minimised, only for testing really
        if (enabled)
        {
            if (focus)
            {
                playerControls.Enable();
            }
            else
            {
                playerControls.Disable();
            }
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandleMenuInput();
        if (PlayerUIManager.Instance.isMenuWindowOpen) return;

        HandlePlayerMovementInput();
        HandleCameraMovementInput();

    }

    private void HandlePlayerMovementInput()
    {
        //get and store the movements from the move input gathered when movement buttons are pressed
        verticalMovement = movementInput.y;
        horizontalMovement = movementInput.x;

        //clamp the movement to set values (as joystick is 0 - 1)

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalMovement) + Mathf.Abs(horizontalMovement));

        if(moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }

        if (player == null) return;

        //we pass in a 0 for horizontal movement because we only want horizontal movement if we are strafing, which i dont even know if i want in this game yet
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
    }

    private void HandleCameraMovementInput()
    {
        cameraVerticalMovement = cameraInput.y;
        cameraHorizontalMovement = cameraInput.x;
    }

    private void HandleMenuInput()
    {
        //when a menu is opened, all locomation based actions should be disabled until closed
        if (openCloseMenuInput)
        {
            openCloseMenuInput = false;
            if(isMenuOpen)
            {
                isMenuOpen = false;
                PlayerUIManager.Instance.playerMenuManager.CloseCharacterMenu();

            }
            else
            {
                PlayerUIManager.Instance.playerMenuManager.OpenCharacterMenu();
                isMenuOpen = true;
            }
        }
    }
}
