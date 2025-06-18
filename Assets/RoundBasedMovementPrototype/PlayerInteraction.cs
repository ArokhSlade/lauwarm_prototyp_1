using UnityEngine;
using UnityEngine.UIElements;

namespace RoundBasedMovementPrototype
{
#nullable enable
    enum RoundPhase
    {
        Planning = 10,
        Executing = 20,
    }

    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] string controllableTag = "Controllable";
        [SerializeField] LayerMask controllableLayerMask;
        [SerializeField] LayerMask groundLayerMask;

        Camera cam;
        RoundPhase currentPhase = RoundPhase.Planning;
        GameObject[] controllableUnits;
        ControllableUnit? selectedControllableUnit;

        void Start()
        {
            cam = Camera.main;
            controllableUnits = GameObject.FindGameObjectsWithTag(controllableTag);
        }

        void Update()
        {
            switch (currentPhase)
            {
                case RoundPhase.Planning:
                    CheckInteraction();
                    break;
                case RoundPhase.Executing:
                    SetSelectedControllableUnit(null);
                    break;
                default:
                    return;
            }
        }

        private void SetSelectedControllableUnit(ControllableUnit? unit)
        {
            if (selectedControllableUnit != null)
            {
                selectedControllableUnit.UnhighlightUnit();
            }

            if (unit == null) return;

            selectedControllableUnit = unit;
            selectedControllableUnit.HighlightUnit();
        }

        private void CheckInteraction()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit controllableHit, 100f, controllableLayerMask))
                {
                    if (!controllableHit.collider.TryGetComponent(out ControllableUnit controllableUnit)) return;

                    Debug.Log($"Controllable at {controllableUnit.transform.position} clicked!");
                    SetSelectedControllableUnit(controllableUnit);
                }
                else if (selectedControllableUnit != null && Physics.Raycast(ray, out RaycastHit groundHit, 100f, groundLayerMask))
                {
                    Debug.Log($"Ground at position {groundHit.point} clicked!");
                    selectedControllableUnit.SetTargetPosition(groundHit.point);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteCommands();
                currentPhase = RoundPhase.Executing;
            }
        }

        void ExecuteCommands()
        {
            foreach (GameObject controllableObject in controllableUnits)
            {
                if (!controllableObject.TryGetComponent(out ControllableUnit controllableUnit)) continue;

                controllableUnit.StartExecution();
            }
        }
    }
}
