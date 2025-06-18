#nullable disable

using TicTacTwo;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Board))]
public class UserInput : MonoBehaviour
{
    [SerializeField] LayerMask boardLayer;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, 100f, boardLayer)) return;

            Debug.Log($"Click on board at world position {hit.point}");
        }
    }
}
