using UnityEngine;
using UnityEngine.AI;

namespace Assets.RoundBasedMovementPrototype
{
#nullable enable
    enum UnitInteractionState
    {
        Idling = 0,
        Selected = 10,
        Targeting = 20,
        Executing = 30,
        Finished = 40,
    }

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Renderer))]
    public class ControllableUnit : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float targetProximityTolerance = 1.5f;
        [SerializeField] Color defaultColor;
        [SerializeField] Color highlightedColor;
        [SerializeField] GameObject targetMarkerPrefab;

        UnitInteractionState interactionState = UnitInteractionState.Idling;
        Vector3? targetPosition;
        NavMeshAgent agent;
        Material currentMaterial;
        GameObject targetMarker;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;

            currentMaterial = GetComponent<Renderer>().material;

            targetMarker = Instantiate(targetMarkerPrefab, Vector3.zero, Quaternion.identity);
            targetMarker.SetActive(false);
        }

        void FixedUpdate()
        {
            if (interactionState == UnitInteractionState.Executing)
            {
                if (targetPosition.HasValue)
                {
                    Debug.Log($"Distance to target: {(targetPosition.Value - transform.position).magnitude}");
                    if ((targetPosition.Value - transform.position).magnitude <= targetProximityTolerance)
                    {
                        interactionState = UnitInteractionState.Finished;
                    }
                }
            }

            if (interactionState == UnitInteractionState.Finished)
            {
                targetPosition = null;
                targetMarker.SetActive(false);
                interactionState = UnitInteractionState.Idling;
            }
        }

        public void SetTargetPosition(Vector3 position)
        {
            targetPosition = position;

            interactionState = UnitInteractionState.Targeting;

            targetMarker.transform.position = targetPosition.Value;
            targetMarker.SetActive(true);
        }

        public void SelectUnit()
        {
            interactionState = UnitInteractionState.Selected;

            UpdateHighlighting();
        }

        public void DeselectUnit()
        {
            interactionState = UnitInteractionState.Idling;

            UpdateHighlighting();
        }

        void UpdateHighlighting()
        {
            currentMaterial.color = interactionState == UnitInteractionState.Selected ? highlightedColor : defaultColor;
        }

        public void StartExecution()
        {
            if (targetPosition is Vector3 target)
            {
                interactionState = UnitInteractionState.Executing;
                agent.SetDestination(target);
            }
        }
    }
}
