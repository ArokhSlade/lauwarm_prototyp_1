#nullable disable

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAgent : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    public void MoveToTarget(Vector3 worldPosition)
    {
        navMeshAgent.destination = worldPosition;
    }
}
