using UnityEngine;
using UnityEngine.AI;

namespace RoundBasedMovementPrototype
{
    enum CharacterState
    {
        Idling = 0,
        Executing = 10,
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class ControllableUnit : MonoBehaviour
    {
        [SerializeField] float speed = 3f;

        CharacterState currentState = CharacterState.Idling;
        Vector3? targetPosition;
        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }

        void Update()
        {
        }

        public void SetTargetPosition(Vector3 position)
        {
            Debug.Log($"Unit target set to {position}");
            targetPosition = position;
        }

        public void StartExecution()
        {
            Debug.Log("Starting command execution!");
            currentState = CharacterState.Executing;

            if (targetPosition is Vector3 target)
            {
                Debug.Log("Setting agent destination...");
                agent.SetDestination(target);
            }

            targetPosition = null;
        }
    }
}
