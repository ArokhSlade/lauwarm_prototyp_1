using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerAgent agent;
    [SerializeField] LayerMask groundLayer;

    Camera cam;
    PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        if (input.LeftMouseButtonClicked)
        {
            Ray ray = cam.ScreenPointToRay(input.MousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, groundLayer))
            {
                agent.MoveToTarget(hit.point);
            }
        }
    }

}