#nullable disable

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, Header("Input Actions")] InputActionAsset inputActionAsset;
    [SerializeField] string inputActionMapName;
    [SerializeField] string mousePositionActionName;
    [SerializeField] string mouseInteractionActionName;

    InputAction mousePositionAction;
    InputAction mouseInteractionAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool LeftMouseButtonClicked => mouseInteractionAction.WasPerformedThisFrame();

    void Awake()
    {
        InputActionMap inputActions = inputActionAsset.FindActionMap(inputActionMapName);
        mousePositionAction = inputActions.FindAction(mousePositionActionName);
        mouseInteractionAction = inputActions.FindAction(mouseInteractionActionName);

        RegisterActions();
    }

    void OnEnable()
    {
        mousePositionAction.Disable();
        mouseInteractionAction.Disable();
    }

    void RegisterActions()
    {
        mousePositionAction.performed += MousePositionActionPerformed;
    }

    void MovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    void MoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    void MousePositionActionPerformed(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
}
