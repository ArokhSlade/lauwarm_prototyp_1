using UnityEngine;

public enum Goal
{
    None = 0,
    Idle = 10,
    Chase = 50,
    Retreat = 60,
}

[RequireComponent(typeof(IdleActionState))]
[RequireComponent(typeof(ChaseActionState))]
[RequireComponent(typeof(RetreatActionState))]
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] Goal initialGoal;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float chaseRadius = 7f;
    [SerializeField] float basePositionTolerance = 1.0f;
    [SerializeField] LayerMask playerLayer;

    Goal currentGoal;
    IState currentActionState;
    Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        currentGoal = initialGoal;
        EnterState();
    }

    void Update()
    {
        EvaluateGoal();
        currentActionState.UpdateState();
    }

    void EvaluateGoal()
    {
        bool isPlayerInDetectionRange = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
        bool isPlayerInChaseRange = Physics.CheckSphere(transform.position, chaseRadius, playerLayer);

        bool isAtBasePosition = (basePosition - transform.position).magnitude <= basePositionTolerance;

        if (currentGoal == Goal.Idle && isPlayerInDetectionRange)
        {
            currentGoal = Goal.Chase;
            EnterState();
        }
        else if (currentGoal == Goal.Chase && !isPlayerInChaseRange)
        {
            currentGoal = Goal.Retreat;
            EnterState();
        }
        else if (currentGoal == Goal.Retreat && isAtBasePosition)
        {
            currentGoal = Goal.Idle;
            EnterState();
        }
    }

    void EnterState()
    {
        switch (currentGoal)
        {
            case Goal.Idle:
                currentActionState = GetComponent<IdleActionState>();
                break;
            case Goal.Chase:
                currentActionState = GetComponent<ChaseActionState>();
                break;
            case Goal.Retreat:
                currentActionState = GetComponent<RetreatActionState>();
                break;
            default:
                Debug.LogError($"Current goal {currentGoal} not handled!");
                break;
        }

        currentActionState.Enter();
    }

    public Vector3 GetBasePosition()
    {
        return basePosition;
    }
}
