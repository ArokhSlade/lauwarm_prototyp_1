#nullable disable

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class ActionState : MonoBehaviour, IState
{
    [SerializeField] protected float speed;

    protected NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void UpdateState();
}
