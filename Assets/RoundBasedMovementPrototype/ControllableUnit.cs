using UnityEngine;
using UnityEngine.AI;

namespace RoundBasedMovementPrototype
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Renderer))]
    public class ControllableUnit : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] Color defaultColor;
        [SerializeField] Color highlightedColor;

        Vector3? targetPosition;
        NavMeshAgent agent;
        Material currentMaterial;
        bool isHighlighted;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;

            currentMaterial = GetComponent<Renderer>().material;
        }

        public void SetTargetPosition(Vector3 position)
        {
            Debug.Log($"Unit target set to {position}");
            targetPosition = position;
        }

        public void HighlightUnit()
        {
            isHighlighted = true;
            UpdateHighlighting();
        }

        public void UnhighlightUnit()
        {
            isHighlighted = false;
            UpdateHighlighting();
        }

        void UpdateHighlighting()
        {
            currentMaterial.color = isHighlighted ? highlightedColor : defaultColor;
        }

        public void StartExecution()
        {
            Debug.Log("Starting command execution!");

            if (targetPosition is Vector3 target)
            {
                Debug.Log("Setting agent destination...");
                agent.SetDestination(target);
            }

            targetPosition = null;
        }
    }
}
